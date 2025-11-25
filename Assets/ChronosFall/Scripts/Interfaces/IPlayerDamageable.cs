using ChronosFall.Scripts.Systems;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IPlayerDamageable
    {
        void PlayerTakeDamage(int damage, Entity.ElementType attackType);
    }
}