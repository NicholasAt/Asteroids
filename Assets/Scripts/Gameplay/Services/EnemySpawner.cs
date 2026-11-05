using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Data.Configs.EnemySpawner;
using Assets.Scripts.Gameplay.Interfaces.Services;
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
        private readonly IWorldBounds _worldBounds;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemyRegistry _enemyRegistry;

        public EnemySpawner(SignalBus signalBus, EnemySpawnerData enemySpawnerData, IWorldBounds worldBounds, IEnemyFactory enemyFactory, IEnemyRegistry enemyRegistry)
        {
            _signalBus = signalBus;
            _spawnerData = enemySpawnerData;
            _worldBounds = worldBounds;
            _enemyFactory = enemyFactory;
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
            _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, _spawnerData.Asteroids, CreateAsteroid);
            _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, _spawnerData.Saucers, CreateSaucer);
        }

        private void CreateFragments(Vector2 pos)
        {
            int count = _spawnerData.Fragments;
            for (int i = 0; i < count; i++)
            {
                float rad = i * (Mathf.PI * 2) / count;
                rad += Mathf.PI * 0.5f;
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                _enemyFactory.Create(EnemyType.Fragment, pos, direction);
            }
        }
        private void CreateSaucer(Vector2 pos, Vector2 _)
        {
            _enemyFactory.Create(EnemyType.Saucer, pos, _);
        }
        private void CreateAsteroid(Vector2 pos, Vector2 direction)
        {
            _enemyFactory.Create(EnemyType.Asteroid, pos, direction);
        }
        private void CalculateAndSpawnAsteroinds()
        {
            int difference = _spawnerData.Asteroids - _enemyRegistry.NotPursuing.Count;
            if (difference > 0)
                _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, difference, CreateAsteroid);
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
                        CreateFragments(signal.Position);
                    else
                        CalculateAndSpawnAsteroinds();

                    break;

                case EnemyType.Saucer:
                    _worldBounds.CalculateRandomPositions(_spawnerData.RandomAngle, 1, CreateSaucer);
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no id [{signal.EnemyType}]");
                    break;
            }
        }
    }
}