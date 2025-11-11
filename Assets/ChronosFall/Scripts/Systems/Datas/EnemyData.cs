using ChronosFall.Scripts.Systems.Base;
using UnityEngine;

namespace ChronosFall.Scripts.Systems.Datas
{
    [CreateAssetMenu(menuName = "Enemy/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int enemyID;
        public float enemyHealth;
        public ElementType myPoint;
        public ElementType myElement;
        public GameObject enemyPrefab;
    }
}