using ChronosFall.Scripts.Enemies;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Enemies.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Enemies.Base
{
    // 敵に必要なスクリプトを自動アタッチ
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        public EnemyData baseEdata;
        private EnemyData _edata;
        private int _currentHealth;

        private void Awake()
        {
            _edata = Instantiate(baseEdata);
            EnemyInit();
        }

        /// <summary>
        /// BASEを読み込み初期化
        /// </summary>
        private void EnemyInit()
        {
            _edata.enemyID = Random.Range(1,int.MaxValue);
            if (_edata.isBoss)
            {
                _edata.maxHealth = Random.Range(2000, 5000);
            }
            else
            {
                _edata.maxHealth = Random.Range(100, 1000);
            }
            _edata.enemySpawnPhase = 1;
            _currentHealth = _edata.maxHealth;
        }
        
        private void Start()
        {
            if (!_edata)
            {
                Debug.LogError($"{name} に EnemyData が設定されていません。");
                return;
            }
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
            Debug.Log($"Enemy has damaged : {damage} damage / now health: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        
        private void Die()
        {
            Debug.Log($"{_edata.enemyName} を殺した [ ID : {_edata.enemyID}");
            Destroy(gameObject);
        }
    }
}