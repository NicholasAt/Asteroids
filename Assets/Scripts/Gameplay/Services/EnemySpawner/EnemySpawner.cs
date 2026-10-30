using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Data.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.EnemySpawner
{
    public class EnemySpawner : IEnemySpawner, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly WorldBoundsData _worldBoundsData;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemyRegistry _enemyRegistry;

        public EnemySpawner(SignalBus signalBus, WorldBoundsData worldBoundsData, IEnemyFactory enemyFactory, IEnemyRegistry enemyRegistry)
        {
            _signalBus = signalBus;
            _worldBoundsData = worldBoundsData;
            _enemyFactory = enemyFactory;
            _enemyRegistry = enemyRegistry;
        }

        public void Start()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(OnEnemyDeath);
            _signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
            Spawn();
        }
        #region Spawn

        private int asteroids = 10;
        private int saucers = 2;
        private void Spawn()
        {
            CalculatePosition(asteroids, CreateAster);
            CalculatePosition(saucers, CreateSaucer);
        }
        private void CalculatePosition(int count, Action<Vector2, Vector2> onCalculate)
        {
            int randomAngle = 150;

            int halfRandomAngle = randomAngle / 2;
            Vector2 halfMap = _worldBoundsData.OutMapSize / 2;

            for (int i = 0; i < count; i++)
            {
                bool upDown = UnityEngine.Random.Range(0, 2) == 1;
                bool forward = UnityEngine.Random.Range(0, 2) == 1;

                Vector2 pos;
                Vector2 rawDirection;

                if (upDown)
                {
                    pos.y = forward ? halfMap.y : -halfMap.y;
                    pos.x = UnityEngine.Random.Range(-halfMap.x, halfMap.x);
                    if (forward)
                    {
                        pos.y = halfMap.y;
                        rawDirection = Vector2.down;
                    }
                    else
                    {
                        pos.y = -halfMap.y;
                        rawDirection = Vector2.up;
                    }
                }
                else
                {
                    pos.x = forward ? halfMap.x : -halfMap.x;
                    pos.y = UnityEngine.Random.Range(-halfMap.y, halfMap.y);
                    if (forward)
                    {
                        pos.x = halfMap.x;
                        rawDirection = Vector2.right;
                    }
                    else
                    {
                        pos.x = -halfMap.x;
                        rawDirection = Vector2.left;
                    }
                }
                float offset = Mathf.Atan2(rawDirection.x, rawDirection.y);
                float randomRad = (UnityEngine.Random.Range(-halfRandomAngle, halfRandomAngle) + 90) * Mathf.Deg2Rad;
                randomRad += offset;

                Vector2 direction = new(Mathf.Cos(randomRad), Mathf.Sin(randomRad));
                onCalculate?.Invoke(pos, direction);
            }
        }
        private void CalculateAndSpawnAsteroinds()
        {
            int difference = asteroids - _enemyRegistry.NotPursuing.Count;
            if (difference > 0)
                CalculatePosition(difference, CreateAster);
        }
        private void CreateFragment(int count, Vector2 pos)
        {
            for (int i = 0; i < count; i++)
            {
                _enemyFactory.CreateFragment(pos, new Vector2(0, i * 2), 1, 1, 0.5f);
            }
        }
        private void CreateSaucer(Vector2 pos, Vector2 _)
        {
            _enemyFactory.CreateSaucer(pos, 1, 1, 0.0f);
        }
        private void CreateAster(Vector2 pos, Vector2 direction)
        {
            _enemyFactory.CreateAsteroid(pos, direction, 1, 1, 0.0f);
        }
        #endregion
        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDeathSignal>(OnEnemyDeath);
        }

        private void OnEnemyDeath(EnemyDeathSignal signal)
        {
            switch (signal.EnemyType)
            {
                case EnemyType.Fragment:
                    CalculateAndSpawnAsteroinds();
                    break;

                case EnemyType.Asteroid:
                    CreateFragment(3, signal.Position);
                    break;

                case EnemyType.Saucer:
                    CalculatePosition(1, CreateSaucer);
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no id [{signal.EnemyType}]");
                    break;
            }
        }
    }
}