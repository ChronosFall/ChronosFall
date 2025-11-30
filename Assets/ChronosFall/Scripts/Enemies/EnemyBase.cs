using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems;
using UnityEngine;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        private EnemyRuntimeData _enemyData;
    
        public void Initialize(int enemyId)
        {
            _enemyData = EnemyManager.Instance.CreateEnemyData(enemyId);
        }
    
        /// <summary>
        /// ダメージを受けた場合
        /// </summary>
        /// <param name="damage">ダメージ数</param>
        /// <param name="elementType">相手の属性</param>
        public void TakeDamage(int damage, ElementType elementType)
        {
            _enemyData.CurrentHealth -= damage;
        
            Debug.Log($"Enemy took {damage} damage from {elementType}! HP: {_enemyData.CurrentHealth}");
        
            if (_enemyData.CurrentHealth <= 0)
            {
                OnDeath();
            }
        }
    
        public EnemyRuntimeData GetEnemyData()
        {
            return _enemyData;
        }
    
        /// <summary>
        /// 敵からの攻撃
        /// </summary>
        /// <param name="player">プレイヤーのInterfaceを呼び出す</param>
        public void AttackPlayer(IDamageablePlayer player)
        {
            var attackerData = _enemyData;
            var defenderData = player.GetCharacterRuntimeData();
        
            int damage = DamageCalculator.CalculateEnemyToPlayer(
                attackerData, 
                defenderData
            );
            
            player.TakeDamage(damage, _enemyData.Resistance);
        }
        
        private void OnDeath()
        {
            Debug.Log($"Enemy {_enemyData.EnemyName} died!");
            // ドロップ処理、経験値付与など
            Destroy(gameObject);
        }
    }
}