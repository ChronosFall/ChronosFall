using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class PlayerHealth : MonoBehaviour, IDamageablePlayer
    {
        private CharacterManager _characterManager;
        public CharacterRuntimeData CharacterData { get; private set; }

        private void Start()
        {
            _characterManager = CharacterManager.Instance;
            UpdateCharacterData();
        }

        public void UpdateCharacterData()
        {
            CharacterData = _characterManager.GetActiveCharacter();
            if (CharacterData == null)
            {
                Debug.LogError("キャラクターデータの取得に失敗！GameManagerが初期化されていない可能性があります");
            }
        }

        public void TakeDamage(int damage, List<ElementType> elementType)
        {
            CharacterData.CurrentHealth -= damage;

            _characterManager.UpdateCharacterHealth(
                CharacterData.CharacterId, 
                CharacterData.CurrentHealth
            );
            
            if (CharacterData.CurrentHealth <= 0)
            {
                _characterManager.OnCharacterDeath(CharacterData.CharacterId);
                UpdateCharacterData();
                
                if (CharacterData != null && _characterManager.aliveCharacterIds.Count > 0)
                {
                    // TODO: キャラモデルの切り替え
                    // SwitchCharacterModel(CharacterData.CharacterId);
                }
                else
                {
                    Debug.LogError("全滅しました！");
                    // TODO: ゲームオーバー処理
                }
            }
        }

        public CharacterRuntimeData GetCharacterRuntimeData()
        {
            return CharacterData;
        }
    }
}