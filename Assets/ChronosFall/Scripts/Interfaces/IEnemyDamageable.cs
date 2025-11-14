using ChronosFall.Scripts.Systems.Enemies.Data;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IEnemyDamageable
    {
        void EnemyTakeDamage(int damage, ElementType attackType);
    }
}