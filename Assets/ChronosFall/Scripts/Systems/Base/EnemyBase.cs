using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Datas;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Systems.Base
{
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        public EnemyData baseEdata;
        private EnemyData _edata;
        private int _currentHealth;

        private void Awake()
        {
            _edata = Instantiate(baseEdata);
        }
        
        private void Start()
        {
            if (!_edata)
            {
                Debug.LogError($"{name} に EnemyData が設定されていません。");
                return;
            }
            
            // init
            _currentHealth = _edata.maxHealth;
            Debug.Log($"DEBUG : {_edata.enemyName} がスポーンしました (HP: {_currentHealth}, 属性: {_edata.enemyElement}, 弱点:{_edata.enemyWeakpoint})");
        }

        /// <summary>
        /// ダメージが敵側に当たる
        /// </summary>
        /// <param name="damage">初期ダメージ数値</param>
        /// <param name="playerAttackElement">プレイヤーの属性</param>
        public void EnemyTakeDamage(int damage, ElementType playerAttackElement)
        {
            // 弱点補正
            if (playerAttackElement == _edata.enemyWeakpoint)
            {
                damage = Mathf.RoundToInt(damage * 1.5f);
            }

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        
        private void Die()
        {
            Debug.Log($"{_edata.enemyName} を殺した [ ID : {_edata.enemyID}");
        }
    }
}