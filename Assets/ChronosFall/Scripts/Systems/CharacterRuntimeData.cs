using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;

namespace ChronosFall.Scripts.Systems
{
    [System.Serializable]
    public class CharacterRuntimeData
    {
        public int CharacterId;
        public string CharacterName;
        public int Level;
        public int MaxHealth;
        public int CurrentHealth;
        public int BaseAtk;
        public int BaseDef;
        public float CritRate;
        public float CritMult;
        public int SkillLevel;
        public ElementType Element;
        
    }
}