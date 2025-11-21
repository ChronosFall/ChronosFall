using ChronosFall.Scripts.Systems.Enemies.Data;
using ChronosFall.Scripts.Systems.Player.Data;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IEnemyDamageable
    {
        void EnemyTakeDamage(int damage, ElementType attackType);
    }
}