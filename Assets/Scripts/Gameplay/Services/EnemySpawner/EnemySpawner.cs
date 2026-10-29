using Assets.Scripts.Data.Enemy;
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
        private readonly IEnemyFactory _enemyFactory;
        public EnemySpawner(SignalBus signalBus, IEnemyFactory enemyFactory)
        {
            _signalBus = signalBus;
            _enemyFactory = enemyFactory;
        }

        public void Start()
        {
            _signalBus.Unsubscribe<EnemyDeathSignal>(OnEnemyDeath);
            _signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
            Spawn();
        }
        #region Spawn

        private void Spawn()
        {
            int randomAngle = 150;
            int asteroids = 50;
            Vector2 mapSize = new Vector2(50, 30);

            int halfRandomAngle = randomAngle / 2;
            Vector2 halfMap = mapSize / 2;

            for (int i = 0; i < asteroids; i++)
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
                _enemyFactory.CreateAsteroid(pos, direction, 1, 1, 0.0f);
            }
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
                case EnemyType.Asteroid:

                    for (int i = 0; i < 3; i++)
                    {
                        _enemyFactory.CreateFragment(signal.Position, new Vector2(0, i * 2), 1, 1, 0.5f);
                    }
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no id [{signal.EnemyType}]");
                    break;
            }
        }
    }
}