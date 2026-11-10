using Assets.Game.Scripts.Data.Configs.Enemy;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Settings
{
    [Serializable]
    public class EnemySettings : IValidatableSettings
    {
        public List<EnemyParams> Enemies = new List<EnemyParams>();
        public EnemySpawnerSettings Spawner = new EnemySpawnerSettings();

        private Dictionary<EnemyType, EnemyParams> _byType;

        public EnemyParams Get(EnemyType type)
        {
            if (_byType == null)
                Build();

            if (_byType.TryGetValue(type, out EnemyParams result))
                return result;

            Debug.LogError($"enemies.json: no entry for [{type}], using defaults");
            return new EnemyParams();
        }

        public bool TryValidate(out string error)
        {
            if (Enemies == null)
            {
                error = "Enemies is null";
                return false;
            }

            foreach (EnemyParams entry in Enemies)
            {
                if (entry == null)
                {
                    error = "Enemies contains null entry";
                    return false;
                }

                if (entry.Mass <= 0f)
                {
                    error = $"Enemies[{entry.Type}].Mass must be greater than 0";
                    return false;
                }

                if (entry.CollisionDamage < 0)
                {
                    error = $"Enemies[{entry.Type}].CollisionDamage must not be negative";
                    return false;
                }
            }

            if (Spawner == null)
            {
                error = "Spawner is null";
                return false;
            }

            if (Spawner.MaxAsteroids < 0 || Spawner.MaxSaucers < 0 || Spawner.FragmentsCount < 0)
            {
                error = "Spawner counts must not be negative";
                return false;
            }

            error = null;
            return true;
        }

        private void Build()
        {
            _byType = new Dictionary<EnemyType, EnemyParams>();
            foreach (EnemyParams entry in Enemies)
            {
                if (Enum.TryParse(entry.Type, true, out EnemyType parsed) == false || parsed == EnemyType.None)
                {
                    Debug.LogError($"enemies.json: unknown enemy type [{entry.Type}]");
                    continue;
                }

                if (_byType.ContainsKey(parsed))
                {
                    Debug.LogError($"enemies.json: duplicate enemy type [{parsed}]");
                    continue;
                }

                _byType.Add(parsed, entry);
            }
        }
    }

    [Serializable]
    public class EnemyParams
    {
        public string Type;
        public int Reward = 10;
        public float Force = 1f;
        public float Mass = 1f;
        public float SpeedLimit = 2f;
        public float Drag = 0f;
        public float AngularVelocity = 0;
        public float AngularDrag = 0;
        public int CollisionDamage = 1;
    }

    [Serializable]
    public class EnemySpawnerSettings
    {
        public int MaxAsteroids = 5;
        public int MaxSaucers = 1;
        public int FragmentsCount = 2;
        public int SpawnRandomAngle = 160;
    }
}