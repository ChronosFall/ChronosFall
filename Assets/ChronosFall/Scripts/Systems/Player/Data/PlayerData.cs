using ChronosFall.Scripts.Systems.Enemies.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "ChronosFall/Player Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("基本情報")]
        public string playerName = "Luca"; // TODO : 動的に変更
        public int maxHealth = 1000;
        
        [Header("属性")]
        public ElementType playerAttackElement = ElementType.Fire; // 属性

        [Header("数値")]
        public float skillRate = 1.2f;
        public float resistanceRate = 1.3f;
        public float critChance = 0.5f;
        
        
    }
}