using ChronosFall.Scripts.Systems;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IEnemyDamageable
    {
        void EnemyTakeDamage(int damage, Entity.ElementType attackType);
    }
}