using ChronosFall.Scripts.Systems.Datas;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IPlayerDamageable
    {
        void PlayerTakeDamage(int damage, ElementType attackType);
    }
}