using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Common;
using Assets.Game.Scripts.Gameplay.Enemy;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly EnemyData _enemyData;
        private readonly EnemySettings _enemySettings;
        private readonly SignalBus _signalBus;
        private readonly PhysicsWorld _physicsWorld;
        private readonly IProgressService _progressService;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public EnemyFactory(DiContainer diContainer, EnemyData enemyData, EnemySettings enemySettings, SignalBus signalBus, PhysicsWorld physicsWorld, IProgressService progressService, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            _diContainer = diContainer;
            _enemyData = enemyData;
            _enemySettings = enemySettings;
            _signalBus = signalBus;
            _physicsWorld = physicsWorld;
            _progressService = progressService;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
        }

        public IEnemyBehaviour Create(EnemyType enemyType)
        {
            EnemyParams parameters = _enemySettings.Get(enemyType);
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                case EnemyType.Fragment:
                    return CreateEnemy(false, enemyType, parameters.Force);


                case EnemyType.Saucer:
                    return CreateSaucer(enemyType, parameters.Force);

                case EnemyType.None:
                default:
                    Debug.LogError($"no type [{enemyType}]");
                    return null;
            }
        }

        private IEnemyBehaviour CreateSaucer(EnemyType enemyType, float force)
        {
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(enemyType);

            Saucer enemy = new Saucer(_progressService, enemyType, false, force, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            physicsBody.Force = force;
            instance.GetComponent<EnemyView>().Init(enemy);
            return enemy;
        }

        private IEnemyBehaviour CreateEnemy(bool isPursuing, EnemyType enemyId, float force)
        {
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(enemyId);

            IEnemyBehaviour enemy = new EnemyBehaviour(enemyId, isPursuing, force, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            instance.GetComponent<EnemyView>().Init(enemy);
            return enemy;
        }

        private (GameObject, PhysicsBody) Instantiate(EnemyType enemyType)
        {
            EnemyConfig config = _enemyData.GetConfig(enemyType);
            EnemyParams parameters = _enemySettings.Get(enemyType);

            GameObject instance = _diContainer.InstantiatePrefab(config.EnemyPrefab);
            instance.ToMoveToActiveScene();
            PhysicsBody physicsBody = new PhysicsBody()
            {
                Layer = CollisionLayer.Enemy,
                CollidesWith = CollisionLayer.Player | CollisionLayer.Bullet,

                AngularVelocity = parameters.AngularVelocity,
                AngularDrag = parameters.AngularDrag,
                SpeedLimit = parameters.SpeedLimit,
                Mass = parameters.Mass,
                Drag = parameters.Drag,
            };

            instance.GetComponent<TransformView>().Init(physicsBody);
            return (instance, physicsBody);
        }
    }
}