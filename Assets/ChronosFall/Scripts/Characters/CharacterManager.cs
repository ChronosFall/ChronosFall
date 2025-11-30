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
            
            Init();
        }

        public void Init()
        {
            // 初期化
            OwnerCharacter = new Dictionary<int, CharacterRuntimeData>();
            currentPartyIds = new List<int>();
            _activeCharacterIndex = 0;
            
            // TODO: JSONから全データ読み込み
            LoadFromJson();
        }

        /// <summary>
        /// Jsonからデータを読み込み
        /// </summary>
        private void LoadFromJson()
        {
            // TODO: 実装
            // 仮データ (テスト用)
            var testCharacter = new CharacterRuntimeData
            {
                CharacterId = 1001,
                Level = 58,
                BaseAtk = 245,
                BaseDef = 180,
                CurrentHealth = 1430,
                MaxHealth = 1430,
                CritRate = 0.15f,
                CritMult = 1.5f,
                SkillLevel = 5,
                Element = ElementType.Fire
            };
            
            OwnerCharacter.Add(testCharacter.CharacterId, testCharacter);
            currentPartyIds.Add(testCharacter.CharacterId);
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