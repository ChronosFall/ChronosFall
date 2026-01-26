using System.Collections.Generic;
using ChronosFall.Scripts.Core;
using ChronosFall.Scripts.Enemies;
using ChronosFall.Scripts.Systems;

namespace ChronosFall.Scripts.Interfaces
{
    public interface IDamageablePlayer
    {
        void TakeDamage(int damage, List<ElementType> elementType);
        CharacterRuntimeData GetCharacterRuntimeData();
    }

    public interface IEnemyDamageable
    {
        void TakeDamage(int damage, ElementType elementType);
        EnemyRuntimeData GetEnemyData();
    }
    public enum ElementType
    {
        None = 0,
        Fire = 1,
        Water = 2,
        Electric = 3
    }
}