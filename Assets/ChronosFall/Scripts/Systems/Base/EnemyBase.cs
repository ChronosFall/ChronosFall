using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Datas;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Systems.Base
{
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        public EnemyData edata;
        private int _currentHealth;

        private void Start()
        {
            if (!edata)
            {
                Debug.LogError($"{name} に EnemyData が設定されていません。");
                return;
            }
            
            // init
            _currentHealth = edata.maxHealth;
            Debug.Log($"DEBUG : {edata.enemyName} がスポーンしました (HP: {_currentHealth}, 属性: {edata.enemyElement}, 弱点:{edata.enemyWeakpoint})");
        }

        /// <summary>
        /// ダメージが敵側に当たる
        /// </summary>
        /// <param name="damage">初期ダメージ数値</param>
        /// <param name="playerAttackElement">プレイヤーの属性</param>
        public void EnemyTakeDamage(int damage, ElementType playerAttackElement)
        {
            // 弱点補正
            if (playerAttackElement == edata.enemyWeakpoint)
            {
                damage = Mathf.RoundToInt(damage * 1.5f);
            }

            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                
            }
        }

        
        private void Die()
        {
            Debug.Log($"{edata.enemyName} を殺した [ ID : {edata.enemyID}");
        }
    }
}