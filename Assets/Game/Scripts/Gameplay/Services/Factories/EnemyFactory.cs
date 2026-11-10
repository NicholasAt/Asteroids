using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Common;
using Assets.Game.Scripts.Gameplay.Enemy;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
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
        private readonly ITickService _tickService;

        public EnemyFactory(DiContainer diContainer, EnemyData enemyData, EnemySettings enemySettings, SignalBus signalBus, PhysicsWorld physicsWorld, IProgressService progressService, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds, ITickService tickService)
        {
            _diContainer = diContainer;
            _enemyData = enemyData;
            _enemySettings = enemySettings;
            _signalBus = signalBus;
            _physicsWorld = physicsWorld;
            _progressService = progressService;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
            _tickService = tickService;
        }

        public IEnemyBehaviour Create(EnemyType enemyType)
        {
            EnemyParams parameters = _enemySettings.Get(enemyType);
            PhysicsBody physicsBody = CreatePhysicsBody(parameters);

            IEnemyBehaviour enemy = CreateBehaviour(enemyType, parameters, physicsBody);
            if (enemy == null)
                return null;

            BindView(enemyType, physicsBody, enemy);
            return enemy;
        }

        private IEnemyBehaviour CreateBehaviour(EnemyType enemyType, EnemyParams parameters, PhysicsBody physicsBody)
        {
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                case EnemyType.Fragment:
                    bool canSplit = enemyType == EnemyType.Asteroid;
                    return new EnemyBehaviour(enemyType, parameters, canSplit, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);

                case EnemyType.Saucer:
                    physicsBody.Force = parameters.Force;
                    return new Saucer(_progressService, _tickService, enemyType, parameters, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);

                case EnemyType.None:
                default:
                    Debug.LogError($"no type [{enemyType}]");
                    return null;
            }
        }

        private PhysicsBody CreatePhysicsBody(EnemyParams parameters)
        {
            return new PhysicsBody()
            {
                Layer = CollisionLayer.Enemy,
                CollidesWith = CollisionLayer.Player | CollisionLayer.Bullet,

                AngularVelocity = parameters.AngularVelocity,
                AngularDrag = parameters.AngularDrag,
                SpeedLimit = parameters.SpeedLimit,
                Mass = parameters.Mass,
                Drag = parameters.Drag,
            };
        }

        private void BindView(EnemyType enemyType, PhysicsBody physicsBody, IEnemyBehaviour enemy)
        {
            EnemyConfig config = _enemyData.GetConfig(enemyType);

            GameObject instance = _diContainer.InstantiatePrefab(config.EnemyPrefab);
            instance.ToMoveToActiveScene();

            instance.GetComponent<TransformView>().Init(physicsBody);
            instance.GetComponent<EnemyView>().Init(enemy);
        }
    }
}