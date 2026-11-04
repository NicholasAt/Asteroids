using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Configs.Enemy
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Enemy/Enemy data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private List<EnemyConfig> _configs;
       public IReadOnlyList<EnemyConfig> Configs=>_configs;
        public EnemyConfig GetConfig(EnemyType enemyType)
        {
            foreach (EnemyConfig cfg in _configs)
            {
                if (cfg.EnemyType == enemyType)
                    return cfg;
            }
            Debug.LogError($"no cfg [{enemyType}]");
            return null;
        }
    }
    [Serializable]
    public class EnemyConfig
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public int Reward { get; private set; }
        [field: SerializeField] public float Force { get; private set; }
        [field: SerializeField] public float Mass { get; private set; }
        [field: SerializeField] public float SpeedLimit { get; private set; }
        [field: SerializeField] public float Drag { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}