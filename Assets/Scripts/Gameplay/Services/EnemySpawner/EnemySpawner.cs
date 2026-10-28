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
        public void Preparation()
        {
            _signalBus.Subscribe<EnemyDeathSignal>(OnEnemyDeath);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyDeathSignal>(OnEnemyDeath);
        }

        private void OnEnemyDeath(EnemyDeathSignal signal)
        {
            switch (signal.EnemyType)
            {
                case EnemyType.Asteroid:

                    for (int i = 0; i < 2; i++)
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