using ChronosFall.Scripts.Characters;
using ChronosFall.Scripts.Enemies;
using RedGirafeGames.Agamotto.Scripts.Runtime;
using UnityEngine;

namespace ChronosFall.Scripts.Systems
{
    [RequireComponent(typeof(TimeStone))]
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);

            // ここで初期化順序を明示的に制御
            InitializeManagers();
        }

        private void InitializeManagers()
        {
            Debug.Log("=== Game Initializing ===");

            // CharacterManager初期化
            CharacterManager.Instance.Initialize();
            Debug.Log("CharacterManager initialized");

            // EnemyManager初期化
            EnemyManager.Instance.Initialize();
            Debug.Log("EnemyManager initialized");

            Debug.Log("=== Game Ready ===");
        }
    }
}