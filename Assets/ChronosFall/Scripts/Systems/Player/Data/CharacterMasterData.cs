using System.Collections.Generic;
using System.Linq;
using ChronosFall.Scripts.Systems.Enemies.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Player.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "ChronosFall/Data/CharacterData")]
    public class CharacterMasterData : ScriptableObject
    {
        public List<CharacterBaseData> characters;
        
        private Dictionary<int, CharacterBaseData> _characterDict;
    
        public void Initialize()
        {
            _characterDict = characters.ToDictionary(c => c.characterId);
        }
    
        public CharacterBaseData GetCharacter(int id)
        {
            return _characterDict[id];
        }
    }
    

    [System.Serializable]
    public class CharacterBaseData
    {
        [Header("Data")]
        public int characterId;
        public string characterName;
        public string characterDescription;
        public int defaultHealth;

        public ElementType characterElementType;

        public int level;
        public float skillRate;
        public float resistanceRate;
        public float critChance;
        public float critMult;
        public float pierce;
    }
}