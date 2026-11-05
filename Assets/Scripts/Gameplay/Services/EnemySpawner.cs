using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Data.Configs.EnemySpawner;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Scripts.Gameplay.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services
{
    public class EnemySpawner : IEnemySpawner, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly EnemySpawnerData _spawnerData;
        private readonly IEnemyPool _enemyPool;
        private readonly IWorldBounds _worldBounds;
        private readonly IEnemyRegistry _enemyRegistry;

        public EnemySpawner(SignalBus signalBus, EnemySpawnerData enemySpawnerData, IEnemyPool enemyPool, IWorldBounds worldBounds, IEnemyRegistry enemyRegistry)
        {
            _signalBus = signalBus;
            _spawnerData = enemySpawnerData;
            _enemyPool = enemyPool;
            _worldBounds = worldBounds;
            _enemyRegistry = enemyRegistry;
        }

        public void Start()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(OnEnemyDeath);
            _signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
            Spawn();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(OnEnemyDeath);
        }
        private void Spawn()
        {
            _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, _spawnerData.Asteroids, SpawnAsteroid);
            _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, _spawnerData.Saucers, SpawnSaucer);
        }

        private void SpawnFragments(Vector2 pos)
        {
            int count = _spawnerData.Fragments;
            for (int i = 0; i < count; i++)
            {
                float rad = i * (Mathf.PI * 2) / count;
                rad += Mathf.PI * 0.5f;
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                _enemyPool.Get(EnemyType.Fragment, pos, direction);
            }
        }
        private void SpawnSaucer(Vector2 pos, Vector2 _)
        {
            _enemyPool.Get(EnemyType.Saucer, pos, _);
        }
        private void SpawnAsteroid(Vector2 pos, Vector2 direction)
        {
            _enemyPool.Get(EnemyType.Asteroid, pos, direction);
        }
        private void CalculateAndSpawnAsteroinds()
        {
            int difference = _spawnerData.Asteroids - _enemyRegistry.NotPursuing.Count;
            if (difference > 0)
                _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, difference, SpawnAsteroid);
        }

        private void OnEnemyDeath(EnemyDeathSignal signal)
        {
            switch (signal.EnemyType)
            {
                case EnemyType.Fragment:
                    CalculateAndSpawnAsteroinds();
                    break;

                case EnemyType.Asteroid:

                    if (signal.IsSplit)
                        SpawnFragments(signal.Position);
                    else
                        CalculateAndSpawnAsteroinds();

                    break;

                case EnemyType.Saucer:
                    _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, 1, SpawnSaucer);
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no id [{signal.EnemyType}]");
                    break;
            }
        }
    }
}