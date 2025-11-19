using ChronosFall.Scripts.Systems.Enemies.Data;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Player.Data
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "ChronosFall/Player Data")]
    public class PlayerData : ScriptableObject
    {
        // TODO : 整理
        [Header("基本情報")]
        public string playerName = "Luca"; // TODO : 動的に変更
        public int maxHealth = 1000;
        
        [Header("属性")]
        public ElementType playerAttackElement = ElementType.Fire; // 属性

        [Header("数値")] 
        public int level = 30;
        public float skillRate = 1.2f;
        public float resistanceRate = 1.3f;
        public float critChance = 0.5f;
        public float critMult = 1.3f;
        public float pierce = 0.0f;

    }
}