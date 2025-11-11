using ChronosFall.Scripts.Systems.Base;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IEnemyDamageable
    {
        void EnemyTakeDamage(float damage, ElementType attackType);
    }
}