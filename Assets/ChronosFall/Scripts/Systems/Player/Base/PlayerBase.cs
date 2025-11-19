using ChronosFall.Scripts.Characters.PlayerControl;
using ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack;
using ChronosFall.Scripts.Systems.Enemies.Data;
using ChronosFall.Scripts.Systems.Player.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Player.Base
{
    // 自動アタッチ
    [RequireComponent(typeof(PlayerAttack))]
    [RequireComponent(typeof(Movement))]
    public class PlayerBase : MonoBehaviour
    {
        public PlayerData basePdata;
        private PlayerData _pdata;
        private int _currentHealth;

        private void Awake()
        {
            _pdata = Instantiate(basePdata);
            PlayerInit();
        }

        /// <summary>
        /// BASEを読み込み初期化
        /// </summary>
        private void PlayerInit()
        {
            _currentHealth = _pdata.maxHealth;
        }

        /// <summary>
        /// プレイヤーにダメージ
        /// </summary>
        /// <param name="damage">初期ダメージ数値</param>
        /// <param name="playerElement">プレイヤーの属性</param>
        public void PlayerTakeDamage(int damage, ElementType playerElement)
        {
            int finalDamage = PlayerCalcu.PlayerTakeDamageCalcu(damage, playerElement);
            _currentHealth -= finalDamage;
            Debug.Log($"${_pdata.playerName} has damaged : {damage} damage / now health: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                PlayerDie();
            }
        }

        
        private void PlayerDie()
        {
            Debug.Log($"{_pdata.playerName} was dead.");
            Destroy(gameObject);
            //TODO : ラグドールとキャラ自動切換を導入
        }
    }
}