using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;

namespace ChronosFall.Scripts.Enemies
{
    [System.Serializable]
    public class EnemyRuntimeData
    {
        public int EnemyId;
        public string EnemyName;
        public int Level;
        public int MaxHealth;
        public int CurrentHealth;
        public int BaseAtk;
        public int BaseDef;
        public bool isBroken;
        public List<ElementType> Weakness = new List<ElementType>();
        public List<ElementType> Resistance = new List<ElementType>();
    }
}