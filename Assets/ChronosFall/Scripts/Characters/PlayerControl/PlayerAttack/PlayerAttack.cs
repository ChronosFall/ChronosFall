using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Datas;
using UnityEngine;

namespace ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack
{
    public class PlayerAttack : MonoBehaviour
    {
        public int attackDamage = 20;
        public ElementType attackType = ElementType.Fire;
        private const float AttackRange = 5f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Enemy has been attacked by player!");
                Ray ray = new Ray(transform.position, transform.forward);
                if (Physics.Raycast(ray, out RaycastHit hit, AttackRange))
                {
                    if(hit.collider.TryGetComponent(out IEnemyDamageable target))
                    {
                        target.EnemyTakeDamage(attackDamage, attackType);
                    }
                }
            }
        }
    }
}