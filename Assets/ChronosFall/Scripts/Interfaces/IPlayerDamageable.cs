using ChronosFall.Scripts.Systems.Enemies.Data;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IPlayerDamageable
    {
        void PlayerTakeDamage(int damage, ElementType attackType);
    }
}