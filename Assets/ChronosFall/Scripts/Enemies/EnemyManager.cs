using System.Collections.Generic;
using ChronosFall.Scripts.Interfaces;
using UnityEngine;

namespace ChronosFall.Scripts.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        // シングルトン
        private static EnemyManager _instance;
        public static EnemyManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<EnemyManager>();
                    
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("EnemyManager");
                        _instance = go.AddComponent<EnemyManager>();
                    }
                }
                return _instance;
            }
        }

        // 敵マスターデータ
        private Dictionary<int, EnemyRuntimeData> _enemyMasterData;

        private void Awake()
        {
            // 重複チェック
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
            Initialize();
        }

        public void Initialize()
        {
            _enemyMasterData = new Dictionary<int, EnemyRuntimeData>();
            
            // TODO: JSONから敵マスターデータ読み込み
            LoadEnemyMasterData();
        }

        private void LoadEnemyMasterData()
        {
            // 仮データ (テスト用)
            var slime = new EnemyRuntimeData
            {
                EnemyId = 2001,
                EnemyName = "スライム",
                Level = 10,
                BaseAtk = 50,
                BaseDef = 30,
                MaxHealth = 10000,
                Weakness = new List<ElementType> { ElementType.Fire, ElementType.Water },
                Resistance = new List<ElementType> { ElementType.Electric }
            };
            var Test = new EnemyRuntimeData
            {
                EnemyId = 2002,
                BaseAtk = 3000,
                BaseDef = 500,
                MaxHealth = 10000,
                EnemyName = "Test",
                Weakness = new List<ElementType> { ElementType.None },
                Resistance = new List<ElementType> { ElementType.Fire, ElementType.Water, ElementType.Electric },
            };
            
            _enemyMasterData.Add(slime.EnemyId, slime);
            _enemyMasterData.Add(Test.EnemyId, Test);
        }

        /// <summary>
        /// 敵IDから戦闘用データ生成
        /// </summary>
        public EnemyRuntimeData CreateEnemyData(int enemyId)
        {
            if (!_enemyMasterData.ContainsKey(enemyId))
            {
                Debug.LogError($"EnemyId {enemyId} が見つかりません！");
                return null;
            }

            // マスターデータからコピー
            EnemyRuntimeData master = _enemyMasterData[enemyId];
            
            return new EnemyRuntimeData
            {
                EnemyId = master.EnemyId,
                EnemyName = master.EnemyName,
                Level = master.Level,
                BaseAtk = master.BaseAtk,
                BaseDef = master.BaseDef,
                MaxHealth = master.MaxHealth,
                CurrentHealth = master.MaxHealth, // 初期値
                Weakness = new List<ElementType>(master.Weakness),
                Resistance = new List<ElementType>(master.Resistance),
                isBroken = false
            };
        }

        /// <summary>
        /// マスターデータ直接取得
        /// </summary>
        public EnemyRuntimeData GetEnemyMasterData(int enemyId)
        {
            if (_enemyMasterData.ContainsKey(enemyId))
            {
                return _enemyMasterData[enemyId];
            }
            Debug.LogError($"EnemyId {enemyId} が見つかりません！");
            return null;
        }
    }
}