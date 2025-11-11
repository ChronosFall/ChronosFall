using UnityEngine;
using ChronosFall.Scripts.Interfaces;


namespace ChronosFall.Scripts.Systems.Base
{
    public enum ElementType
    {
        None, Fire, Ice, Wind, Thunder
    }
    
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        [Header("基本ステータス")] 
        public float maxHealth = 100f; // デフォルト最大HP
        private float _currentHealth;
        [Header("属性/弱点設定")]
        public ElementType myPoint =  ElementType.None;
        public ElementType myElement =  ElementType.None;
        [Header("状態")]
        public bool isDead = false;
        public bool isBoss = false;

        protected void EnemySetting()
        {
            _currentHealth = maxHealth;
        }

        /// <summary>
        /// ダメージ処理
        /// </summary>
        /// <param name="damage">ダメージ量</param>
        /// <param name="attackType">攻撃タイプ</param>
        public void EnemyTakeDamage(float damage, ElementType attackType)
        {
            if (isDead) return;
            
            // 弱点属性ならダメージx2
            if (attackType == myPoint) damage *= 2;
            
            _currentHealth -= damage;

            if (_currentHealth <= 0) EnemyDie();
        }
        
        /// <summary>
        /// 死亡機構
        /// </summary>
        private void EnemyDie()
        {
            isDead = true;
            Debug.Log($"{gameObject.name} was dead.");
        }
    }
}