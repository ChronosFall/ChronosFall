using ChronosFall.Scripts.Interfaces;
using ChronosFall.Scripts.Systems.Base;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace ChronosFall.Scripts.Characters.PlayerControl.PlayerAttack
{
    public class PlayerAttack : MonoBehaviour
    {
        public float attackDamage = 20f;
        public ElementType attackType = ElementType.Fire;
        private const float AttackRange = 3f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
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