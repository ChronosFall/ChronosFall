using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems;
using UnityEngine;

namespace ChronosFall.Scripts.Characters
{
    
    public class CharacterManager : MonoBehaviour
    {
        // シングルトン
        private static CharacterManager _instance;
        private CharacterRuntimeData _characterRuntimeData;
        
        public static CharacterManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // シーン内から探す
                    _instance = FindObjectOfType<CharacterManager>();
                    
                    // なければ新規作成
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("CharacterManager");
                        _instance = go.AddComponent<CharacterManager>();
                    }
                }
                return _instance;
            }
        }

        public Dictionary<int, CharacterRuntimeData> OwnerCharacter;
        public List<int> currentPartyIds;
        private int _activeCharacterIndex;

        private void Awake()
        {
            // シングルトンの重複チェック
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移しても破棄されない
        }

        public void Initialize()
        {
            // 初期化
            OwnerCharacter = new Dictionary<int, CharacterRuntimeData>();
            currentPartyIds = new List<int>();
            _activeCharacterIndex = 0;
            
            LoadFromJson();
        }

        // TODO : Json形式ではなくする
        /// <summary>
        /// Jsonからデータを読み込み
        /// </summary>
        private void LoadFromJson()
        {
            var characterDatas = Resources.LoadAll<TextAsset>("Databases/Characters");

            foreach (var characterData in characterDatas)
            {
                Debug.Log($"読み込み中: {characterData.name}");
    
                // JSONをデシリアライズ
                CharacterRuntimeData data = JsonUtility.FromJson<CharacterRuntimeData>(characterData.text);
                
                if (data == null)
                {
                    Debug.LogError($"{characterData.name}: JSONパースに失敗");
                    continue;
                }
    
                if (OwnerCharacter.ContainsKey(data.CharacterId))
                {
                    Debug.LogWarning($"ID重複検知 :  {data.CharacterId} ({characterData.name})");
                    continue;
                }
    
                OwnerCharacter.Add(data.CharacterId, data);
                currentPartyIds.Add(data.CharacterId);
    
                Debug.Log($"読み込み完了 :  {data.CharacterName} (ID: {data.CharacterId}, Lv.{data.Level})");
            }
            Debug.Log($"=== 読み込み完了: {OwnerCharacter.Count}体 ===");
        }

        /// <summary>
        /// 現在操作中のキャラデータを取得
        /// </summary>
        /// <returns>現在操作中のキャラクターデータ</returns>
        public CharacterRuntimeData GetActiveCharacter()
        {
            // 現在のインデックスから取得
            if (currentPartyIds.Count == 0)
            {
                Debug.LogError("パーティにキャラクターがいません！");
                return null;
            }
            
            int activeCharacterId = currentPartyIds[_activeCharacterIndex];
            return OwnerCharacter[activeCharacterId];
        }

        /// <summary>
        /// IDから直接キャラデータを取得
        /// </summary>
        /// <param name="characterId">キャラクターID</param>
        /// <returns>存在する場合CharacterRuntimeDataが返る</returns>
        public CharacterRuntimeData GetCharacterById(int characterId)
        {
            if (OwnerCharacter.ContainsKey(characterId))
            {
                return OwnerCharacter[characterId];
            }
            Debug.LogError($"CharacterId {characterId} が見つかりません！");
            return null;
        }

        /// <summary>
        /// パーティメンバーをインデックスで取得
        /// </summary>
        /// <param name="index">パーティーからのインデックス</param>
        /// <returns>存在する場合CharacterRuntimeData</returns>
        public CharacterRuntimeData GetPartyMember(int index)
        {
            if (index < 0 || index >= currentPartyIds.Count)
            {
                Debug.LogError($"パーティインデックス {index} が範囲外です！");
                return null;
            }
            return GetCharacterById(currentPartyIds[index]);
        }

        /// <summary>
        /// Jsonにデータを保存
        /// </summary>
        /// <param name="characterId">キャラクターID</param>
        /// <param name="health">HP</param>
        public void UpdateCharacterHealth(int characterId, int health)
        {
            if (OwnerCharacter.ContainsKey(characterId))
            {
                OwnerCharacter[characterId].CurrentHealth = health;
                // TODO: JSONに保存
            }
        }

        /// <summary>
        /// キャラクターが死亡した場合
        /// </summary>
        public void OnCharacterDeath()
        {
            Debug.LogWarning("キャラクターが死亡しました！");
            // TODO: ゲームオーバー or 次のキャラに交代
            
            // 次のキャラに自動交代する例
            if (currentPartyIds.Count > 1)
            {
                SwitchNextPlayerCharacter();
            }
            else
            {
                // パーティ全滅
                Debug.LogError("パーティ全滅！");
            }
        }
        
        public int GetActiveCharacterIndex()
        {
            return _activeCharacterIndex;
        }

        /// <summary>
        /// キャラクターチェンジ
        /// </summary>
        /// <param name="index">インデックス番号</param>
        public void SwitchPlayerCharacter(int index)
        {
            if (index < 0 || index >= currentPartyIds.Count)
            {
                Debug.LogError($"交代先のインデックス {index} が無効です！");
                return;
            }
            
            _activeCharacterIndex = index;
            Debug.Log($"キャラ交代: {GetActiveCharacter().CharacterId}");
        }

        public void SwitchPreviousPlayerCharacter()
        {
            int newIndex = _activeCharacterIndex - 1;
            if (newIndex < 0) newIndex = currentPartyIds.Count - 1; // ループ
            SwitchPlayerCharacter(newIndex);
        }

        public void SwitchNextPlayerCharacter()
        {
            int newIndex = _activeCharacterIndex + 1;
            if (newIndex >= currentPartyIds.Count) newIndex = 0; // ループ
            SwitchPlayerCharacter(newIndex);
        }
    }
}