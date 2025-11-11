using System.Collections.Generic;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Datas
{
    [CreateAssetMenu(fileName = "New Enemy Data", menuName = "ChronosFall/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [Header("基本情報")]
        public string enemyName = "Unnamed";
        public int enemyID = Random.Range(0, int.MaxValue); // TODO : そのうちしっかりしたID機構を作成
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