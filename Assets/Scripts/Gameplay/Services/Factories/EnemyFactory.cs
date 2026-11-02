using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Enemy.Fragments;
using Assets.Scripts.Data.Enemy.Saucer;
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
        private readonly AsteroidData _asteroidData;
        private readonly FragmentsData _fragmentsData;
        private readonly SaucerData _saucerData;
        private readonly SignalBus _signalBus;
        private readonly PhysicsWorld _physicsWorld;
        private readonly IProgressService _progressService;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public EnemyFactory(DiContainer diContainer, AsteroidData asteroidData, FragmentsData fragmentsData, SaucerData saucerData, SignalBus signalBus, PhysicsWorld physicsWorld, IProgressService progressService, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            _diContainer = diContainer;
            _asteroidData = asteroidData;
            _fragmentsData = fragmentsData;
            _saucerData = saucerData;
            _signalBus = signalBus;
            _physicsWorld = physicsWorld;
            _progressService = progressService;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            GameObject prefab = _asteroidData.AsteroidPrefab;
            CreateEnemy(false, EnemyType.Asteroid, prefab, position, direction, force, speedLimit, drag);
        }

        public void CreateFragment(Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            GameObject prefab = _fragmentsData.FragmentPrefab;
            CreateEnemy(false, EnemyType.Fragment, prefab, position, direction, force, speedLimit, drag);
        }

        public void CreateSaucer(Vector2 position, float force, float speedLimit, float drag)
        {
            GameObject prefab = _saucerData.SaucerPrefab;
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(prefab, position, Vector2.zero, force, speedLimit, drag);

            Saucer enemy = new Saucer(EnemyType.Saucer, _progressService, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            InitAndRegister(true, enemy, instance);
        }

        private void CreateEnemy(bool isPursuing, EnemyType enemyId, GameObject prefab, Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            (GameObject instance, PhysicsBody physicsBody) = Instantiate(prefab, position, direction, force, speedLimit, drag);

            IEnemyBehaviour enemy = new EnemyBehaviour(enemyId, _physicsWorld, physicsBody, _signalBus, _enemyRegistry, _worldBounds);
            InitAndRegister(isPursuing, enemy, instance);
        }

        private (GameObject, PhysicsBody) Instantiate(GameObject prefab, Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
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
                Mass = _asteroidData.Mass,
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