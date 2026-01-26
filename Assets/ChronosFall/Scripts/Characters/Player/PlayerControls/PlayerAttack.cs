using System.Collections.Generic;
using ChronosFall.Scripts.Core;
using ChronosFall.Scripts.Core.Configs;
using ChronosFall.Scripts.Interfaces;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.Player.PlayerControls
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float attackRange = 5f; // 攻撃距離
        [SerializeField] private float attackAngel = 40f; // 攻撃範囲
        [SerializeField] private float minStep = 5f; // 最小ステップ角度
     
        private PlayerHealth _playerHealth;
        private readonly List<GameObject> _attackedEnemies = new(); // 攻撃した敵List

        private void Start()
        {
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(CharacterInputKey.PlayerAttack))
            {
                ExecuteAttack();
            }
        }

        private void ExecuteAttack()
        {
            var attackerData = _playerHealth.CharacterData;
            if (attackerData == null) return;
            
            _attackedEnemies.Clear();

            // Rayの本数を計算 : 最低2本、扇の角度 / 最小ステップ数 + 1 本のRayCastが呼ばれる
            int rayCount = Mathf.Max(2, Mathf.CeilToInt(attackAngel / minStep) + 1);
            // 各Raycastの角度間隔を計算 
            float each = attackAngel / (rayCount - 1);

            for (int i = 0; i < rayCount; i++)
            {
                // 各Rayの角度を計算
                float angle = -attackAngel / 2f + each * i;
                // 計算した角度でRayの方向ベクトルを作成
                Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;

                if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, attackRange))
                {
                    if (hit.collider.TryGetComponent(out IEnemyDamageable enemy))
                    {
                        // 2連続の攻撃判定が入らないように
                        if (_attackedEnemies.Contains(hit.collider.gameObject)) continue;

                        var defenderData = enemy.GetEnemyData();
                        int damage = DamageCalculator.CalculatePlayerToEnemy(
                            attackerData,
                            defenderData
                        );

                        // プレイヤーの情報を渡す
                        enemy.TakeDamage(damage, attackerData.Element);

                        _attackedEnemies.Add(hit.collider.gameObject);
                    }
                }
            }
        }
    }
}