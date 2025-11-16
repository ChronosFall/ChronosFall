using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Enemies.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack
{
    public class PlayerAttack : MonoBehaviour
    {
        public int attackDamage = 20; // ダメージ TODO : そのうち動的に変更
        public ElementType attackType = ElementType.Fire; // プレイヤーの属性 TODO : ここもそのうち動的に変更
        private const float AttackRange = 5f; // 攻撃距離
        private const float AttackAngel = 40f; // 攻撃範囲
        private const float MinStep = 5f; // 最小ステップ角度
        private List<GameObject> _attackedEnemies; // 攻撃した敵List

        private void Start()
        {
            _attackedEnemies = new List<GameObject>();
            _attackedEnemies.Clear();
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 攻撃範囲を最小ステップ角度刻みで割ったときに必要なRay数を計算する
                int rayCount = Mathf.Max(2, Mathf.CeilToInt(AttackAngel / MinStep) + 1);

                // そのRay数で均等な角度に配置するため、1本ごとの角度間隔を求める
                float each = AttackAngel / (rayCount - 1);
                
                for (int i = 0; i < rayCount; i++)
                {
                    // 初期を-20°に設定しそこからeachごと足していく
                    float angle = -AttackAngel / 2f + each * i;
                    Vector3 dir = Quaternion.Euler(0, angle, 0) * transform.forward;
                    
                    if (Physics.Raycast(transform.position + Vector3.up, dir, out RaycastHit hit, AttackRange))
                    {
                        if (hit.collider.TryGetComponent(out IEnemyDamageable target))
                        {
                            // 2連続の攻撃判定が入らないように
                            if (_attackedEnemies.Contains(hit.collider.gameObject)) continue;
                            target.EnemyTakeDamage(attackDamage, attackType);
                            Debug.Log($"Enemy has been attacked by player! TARGET : ${target}");
                            _attackedEnemies.Add(hit.collider.gameObject);
                        }
                    }
                }
                _attackedEnemies.Clear();
            }
        }
    }
}