using ChronosFall.Scripts.Core;
using ChronosFall.Scripts.Core.Configs;
using ChronosFall.Scripts.Systems.UI;
using RedGirafeGames.Agamotto.Scripts.Runtime.Agents;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    // 必須スクリプト
    [RequireComponent(typeof(ChangeConfig))] // コンフィグ
    [RequireComponent(typeof(TimeControl))] // プレイヤー時間操作
    [RequireComponent(typeof(GamePauseMenu))] // ゲーム一時停止メニュー
    [RequireComponent(typeof(PlayerMovement))] // 移動
    [RequireComponent(typeof(PlayerAnimation))] // アニメーション
    [RequireComponent(typeof(PlayerHealth))] // 体力管理
    [RequireComponent(typeof(PlayerAttack))] // プレイヤー攻撃
    [RequireComponent(typeof(PlayerInteraction))] // プレイヤーインタラクション
    
    // 必須コンポーネント
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(TimeAgent))]
    
    public class PlayerController : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private CharacterManager _characterManager;

        // 初期化
        private void Start()
        {
            _characterManager = CharacterManager.Instance;
            
            _playerHealth = GetComponent<PlayerHealth>();

            ChangeConfig.isReset = true;
        }

        // メイン処理
        private void Update()
        {
            // キャラクター切り替え
            if (Input.GetKeyDown(CharacterInputKey.NextCharacter))
            {
                _characterManager.SwitchNextPlayerCharacter();
                _playerHealth.UpdateCharacterData();
            }

            if (Input.GetKeyDown(CharacterInputKey.PreviousCharacter))
            {
                _characterManager.SwitchPreviousPlayerCharacter();
                _playerHealth.UpdateCharacterData();
            }
        }
    }
}