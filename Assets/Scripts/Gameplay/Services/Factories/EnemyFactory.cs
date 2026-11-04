using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Enemy;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly EnemyData _enemyData;
        private readonly SignalBus _signalBus;
        private readonly PhysicsWorld _physicsWorld;
        private readonly IProgressService _progressService;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public EnemyFactory(DiContainer diContainer, EnemyData enemyData, SignalBus signalBus, PhysicsWorld physicsWorld, IProgressService progressService, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            _diContainer = diContainer;
            _enemyData = enemyData;
            _signalBus = signalBus;
            _physicsWorld = physicsWorld;
            _progressService = progressService;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
        }

        public void Create(EnemyType enemyType, Vector2 position, Vector2 direction)
        {
            EnemyConfig config = _enemyData.GetConfig(enemyType);
            switch (enemyType)
            {
                case EnemyType.Asteroid:
                case EnemyType.Fragment:
                    CreateEnemy(false, enemyType, config.EnemyPrefab, position, direction, config.Force, config.Mass, config.SpeedLimit, config.Drag);
                    break;

                case EnemyType.Saucer:
                    CreateSaucer(enemyType, config.EnemyPrefab, position, config.Force, config.Mass, config.SpeedLimit, config.Drag);
                    break;

                case EnemyType.None:
                default:
                    Debug.LogError($"no type [{enemyType}]");
                    break;
            }
        }

        private void CreateSaucer(EnemyType enemyType, GameObject prefab, Vector2 position, float force, float mass, float speedLimit, float drag)
        {
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(prefab, position, Vector2.zero, force, mass, speedLimit, drag);

            Saucer enemy = new Saucer(enemyType, _progressService, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            InitAndRegister(true, enemy, instance);
        }

        private void CreateEnemy(bool isPursuing, EnemyType enemyId, GameObject prefab, Vector2 position, Vector2 direction, float force, float mass, float speedLimit, float drag)
        {
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(prefab, position, direction, force, mass, speedLimit, drag);

            IEnemyBehaviour enemy = new EnemyBehaviour(enemyId, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            InitAndRegister(isPursuing, enemy, instance);
        }

        private (GameObject, PhysicsBody) Instantiate(GameObject prefab, Vector2 position, Vector2 direction, float force, float mass, float speedLimit, float drag)
        {
            GameObject instance = _diContainer.InstantiatePrefab(prefab);
            instance.ToMoveToActiveScene();

            instance.transform.position = position;
            instance.transform.up = direction;

            PhysicsBody physicsBody = new PhysicsBody()
            {
                Layer = CollisionLayer.Enemy,
                CollidesWith = CollisionLayer.Player | CollisionLayer.Bullet,

                Velocity = direction * force,
                SpeedLimit = speedLimit,
                Direction = direction,
                Position = position,
                Drag = drag,
                Mass = mass,
            };

            _physicsWorld.Add(physicsBody);
            instance.GetComponent<TransformView>().Init(physicsBody);
            return (instance, physicsBody);
        }

        private void InitAndRegister(bool isPursuing, IEnemyBehaviour enemy, GameObject instance)
        {
            _enemyRegistry.Add(isPursuing, enemy);
            _worldBounds.AddOutMap(enemy);
            instance.GetComponent<EnemyView>().Init(enemy);
        }
    }
}