using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChronosFall.Scripts.Systems.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "ChronosFall/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        // TODO : 整理
        [Header("基本情報")]
        public string enemyName = "Unnamed";
        public int enemyID = 0; // TODO : そのうちしっかりしたID機構を作成
        public int enemySpawnPhase = 1; // ここも
        public int maxHealth = 100;
        public float moveSpeed = 3f;
        public int level = 10;
        public int def = 200;
        public bool isBroken = false;

        [Header("属性")]
        public ElementType enemyElement = ElementType.None; // 敵の属性
        public List<ElementType> enemyWeakpoint = new List<ElementType> { ElementType.None }; // 敵の弱点
        public List<ElementType> enemyResistancePoint = new List<ElementType> { ElementType.None }; // 敵の耐性
        
        [Header("設定")]
        public bool isBoss = false;
        public List<int> enemyEffectID = new List<int>();
    }

    public enum ElementType
    {
        None,
        Fire,
        Ice,
        Thunder,
        Wind,
    }
}