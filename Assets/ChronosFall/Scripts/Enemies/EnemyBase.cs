using System.Collections;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Enemies
{
    [RequireComponent(typeof(EnemyMovement))]
    public class EnemyBase : MonoBehaviour, IEnemyDamageable
    {
        private EnemyRuntimeData _enemyData;
        [SerializeField] public int selectedEnemyId = 2001;

        private void Awake()
        {
            Initialize(selectedEnemyId);
        }
        private void Initialize(int enemyId)
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
        
        private bool _isAttacked; // 攻撃したかのフラグ
        private const float AttackAngle = 70f;      // 扇形の角度 (/2 °)
        private const float AttackRange = 3f;      // 攻撃距離
        private const float MinSteps = 5f;         // 最小ステップ角度
        
        private void Update()
        {
            // Ray本数を計算 (角度ベース)
            int rayCount = Mathf.CeilToInt(AttackAngle / MinSteps) + 1;
    
            // 各Rayの角度間隔を計算
            float each = AttackAngle / (rayCount - 1);

            for (int i = 0; i < rayCount; i++)
            {
                // 各Rayの角度を計算 (+-35°)
                float angle = -AttackAngle / 2f + each * i;
        
                // 計算した角度でRayの方向ベクトルを作成
                Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;

                // Raycast発射 (距離ベース)
                if (Physics.Raycast(transform.position + Vector3.zero, dir, out RaycastHit hit, AttackRange))
                {
                    if (hit.collider.TryGetComponent(out IDamageablePlayer player))
                    {
                        StartCoroutine(Attack(player));
                    }
                }
        
                // デバッグ用（Sceneビューで確認）
                Debug.DrawRay(transform.position + Vector3.up, dir * AttackRange, Color.red, 0.5f);
            }
        }
        
        /// <summary>
        /// 攻撃
        /// </summary>
        private IEnumerator Attack(IDamageablePlayer player)
        {
            _isAttacked = true;
            // 攻撃までの待機時間
            var waitTime = Random.Range(1, 5);
            yield return new WaitForSeconds(waitTime);
            if (!_isAttacked) yield break; // 攻撃中なら停止
            AttackPlayer(player);
            _isAttacked = false;
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