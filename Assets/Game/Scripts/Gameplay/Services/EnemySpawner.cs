using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Game.Scripts.Gameplay.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services
{
    public class EnemySpawner : IEnemySpawner, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly EnemySpawnerSettings _spawnerSettings;
        private readonly int _maxEnemies;
        private readonly IEnemyPool _enemyPool;
        private readonly IWorldBounds _worldBounds;
        private readonly IEnemyRegistry _enemyRegistry;

        public EnemySpawner(SignalBus signalBus, EnemySettings enemySettings, WorldSettings worldSettings, IEnemyPool enemyPool, IWorldBounds worldBounds, IEnemyRegistry enemyRegistry)
        {
            _signalBus = signalBus;
            _spawnerSettings = enemySettings.Spawner;
            _maxEnemies = worldSettings.MaxEnemies;
            _enemyPool = enemyPool;
            _worldBounds = worldBounds;
            _enemyRegistry = enemyRegistry;
        }

        public void Start()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(OnEnemyDeath);
            _signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);

            if (_spawnerSettings.MaxAsteroids + _spawnerSettings.MaxSaucers > _maxEnemies)
                Debug.LogWarning($"Spawner wants {_spawnerSettings.MaxAsteroids + _spawnerSettings.MaxSaucers} enemies, but world.json maxEnemies = {_maxEnemies}");

            Spawn();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(OnEnemyDeath);
        }

        private bool HasFreeSlot()
        {
            return _enemyRegistry.Count < _maxEnemies;
        }

        private void Spawn()
        {
            _worldBounds.CalculateRandomPositions(_spawnerSettings.SpawnRandomAngle, _spawnerSettings.MaxAsteroids, SpawnAsteroid);
            _worldBounds.CalculateRandomPositions(_spawnerSettings.SpawnRandomAngle, _spawnerSettings.MaxSaucers, SpawnSaucer);
        }

        private void SpawnFragments(Vector2 pos)
        {
            int count = _spawnerSettings.FragmentsCount;
            for (int i = 0; i < count; i++)
            {
                if (HasFreeSlot() == false)
                    return;

                Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
                _enemyPool.Get(EnemyType.Fragment, pos, direction);
            }
        }

        private void SpawnSaucer(Vector2 pos, Vector2 _)
        {
            if (HasFreeSlot() == false)
                return;

            _enemyPool.Get(EnemyType.Saucer, pos, _);
        }

        private void SpawnAsteroid(Vector2 pos, Vector2 direction)
        {
            if (HasFreeSlot() == false)
                return;

            _enemyPool.Get(EnemyType.Asteroid, pos, direction);
        }

        private void CalculateAndSpawnAsteroids()
        {
            int difference = _spawnerSettings.MaxAsteroids - _enemyRegistry.GetCount(EnemyType.Asteroid);
            if (difference > 0)
                _worldBounds.CalculateRandomPositions(_spawnerSettings.SpawnRandomAngle, difference, SpawnAsteroid);
        }

        private void OnEnemyDeath(EnemyDeathSignal signal)
        {
            switch (signal.EnemyType)
            {
                case EnemyType.Fragment:
                    CalculateAndSpawnAsteroids();
                    break;

                case EnemyType.Asteroid:

                    if (signal.SpawnFragments)
                        SpawnFragments(signal.Position);
                    else
                        CalculateAndSpawnAsteroids();

                    break;

                case EnemyType.Saucer:
                    _worldBounds.CalculateRandomPositions(_spawnerSettings.SpawnRandomAngle, 1, SpawnSaucer);//respawn of a single enemy
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no id [{signal.EnemyType}]");
                    break;
            }
        }
    }
}