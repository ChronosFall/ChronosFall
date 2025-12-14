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
        public List<int> _aliveCharacterIds;
        private int _activeCharacterId;

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
            _aliveCharacterIds = new List<int>();
            _activeCharacterId = 0;
            
            LoadFromJson();
            
            if (_aliveCharacterIds.Count > 0)
            {
                _activeCharacterId = _aliveCharacterIds[0];
            }
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
                _aliveCharacterIds.Add(data.CharacterId);
    
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
            
            // IDで直接取得
            if (OwnerCharacter.ContainsKey(_activeCharacterId))
            {
                return OwnerCharacter[_activeCharacterId];
            }
            
            Debug.LogError($"アクティブキャラID {_activeCharacterId} が見つかりません！");
            return null;
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
        public void OnCharacterDeath(int characterId)
        {
            Debug.LogWarning($"キャラクター {characterId} が死亡しました！");
            
            // 生存リストから削除
            _aliveCharacterIds.Remove(characterId);
            
            // 死んだキャラが現在操作中だった場合のみ交代
            if (_activeCharacterId == characterId)
            {
                if (_aliveCharacterIds.Count > 0)
                {
                    // 次の生存キャラに交代
                    _activeCharacterId = _aliveCharacterIds[0];
                    Debug.Log($"自動交代: {GetActiveCharacter()?.CharacterName ?? "不明"}");
                }
                else
                {
                    Debug.LogError("パーティ全滅！");
                }
            }
        }
        
        public int GetActiveCharacterId()
        {
            return _activeCharacterId;
        }

        /// <summary>
        /// キャラクターチェンジ
        /// </summary>
        /// <param name="index">インデックス番号</param>
        private void SwitchPlayerCharacter(int index)
        {
            if (index < 0 || index >= _aliveCharacterIds.Count)
            {
                Debug.LogError($"交代先のインデックス {index} が無効です！");
                return;
            }
            
            // 生存リストから取得
            int targetCharacterId = _aliveCharacterIds[index];
            
            _activeCharacterId = targetCharacterId;
            Debug.Log($"キャラ交代: {GetActiveCharacter()?.CharacterName ?? "不明"}");
        }

        public void SwitchPreviousPlayerCharacter()
        {
            if (_aliveCharacterIds.Count == 0) return;

            // 現在のIDの位置を取得
            int currentIndex = _aliveCharacterIds.IndexOf(_activeCharacterId);
            if (currentIndex == -1) currentIndex = 0;

            int newIndex = currentIndex - 1;
            if (newIndex < 0) newIndex = _aliveCharacterIds.Count - 1; // ループ

            SwitchPlayerCharacter(newIndex);

        }

        public void SwitchNextPlayerCharacter()
        {
            if (_aliveCharacterIds.Count == 0) return;
        
            // 現在のIDの位置を取得
            int currentIndex = _aliveCharacterIds.IndexOf(_activeCharacterId);
            if (currentIndex == -1) currentIndex = 0;
        
            int newIndex = currentIndex + 1;
            if (newIndex >= _aliveCharacterIds.Count) newIndex = 0; // ループ
        
            SwitchPlayerCharacter(newIndex);
        }
    }
}