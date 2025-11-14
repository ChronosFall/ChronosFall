using System.Collections.Generic;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Enemies.Data
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "ChronosFall/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("基本情報")]
        public string enemyName = "Unnamed";
        public int enemyID = 0; // TODO : そのうちしっかりしたID機構を作成
        public int enemySpawnPhase = 1; // ここも
        public int maxHealth = 100;
        public float moveSpeed = 3f;

        [Header("属性")]
        public ElementType enemyElement = ElementType.None; // 敵の属性
        public ElementType enemyWeakpoint = ElementType.None; // 敵の弱点

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