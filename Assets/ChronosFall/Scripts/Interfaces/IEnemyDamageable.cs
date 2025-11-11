using ChronosFall.Scripts.Systems.Datas;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IEnemyDamageable
    {
        void EnemyTakeDamage(int damage, ElementType attackType);
    }
}