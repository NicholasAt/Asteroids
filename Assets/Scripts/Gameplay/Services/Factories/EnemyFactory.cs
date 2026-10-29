using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Enemy.Fragments;
using Assets.Scripts.Data.Enemy.Saucer;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Enemy;
using Assets.Scripts.Gameplay.Extensions;
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

        public EnemyFactory(DiContainer diContainer, AsteroidData asteroidData, FragmentsData fragmentsData, SaucerData saucerData)
        {
            _diContainer = diContainer;
            _asteroidData = asteroidData;
            _fragmentsData = fragmentsData;
            _saucerData = saucerData;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            GameObject prefab = _asteroidData.AsteroidPrefab;
            CreateEnemy(EnemyType.Asteroid, prefab, position, direction, force, speedLimit, drag);
        }

        public void CreateFragment(Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            GameObject prefab = _fragmentsData.FragmentPrefab;
            CreateEnemy(EnemyType.Fragment, prefab, position, direction, force, speedLimit, drag);
        }

        public void CreateSaucer(Vector2 position, float force, float speedLimit, float drag)
        {
            ICharacterRegister characterRegister = Resolve<ICharacterRegister>();
            GameObject prefab = _saucerData.SaucerPrefab;
            (GameObject instance, PhysicsWorld physicsWorld, PhysicsBody physicsBody) = Instantiate(prefab, position, Vector2.zero, force, speedLimit, drag);

            Saucer enemy = new Saucer(EnemyType.Saucer, characterRegister, physicsWorld, physicsBody, Resolve<SignalBus>());

            enemy.Init();
            instance.GetComponent<EnemyView>().Init(enemy);
        }

        private void CreateEnemy(EnemyType enemyType, GameObject prefab, Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            (GameObject instance, PhysicsWorld physicsWorld, PhysicsBody physicsBody) = Instantiate(prefab, position, direction, force, speedLimit, drag);

            Enemy.Enemy enemy = new Enemy.Enemy(enemyType, physicsWorld, physicsBody, Resolve<SignalBus>());
            enemy.Init();

            instance.GetComponent<EnemyView>().Init(enemy);
        }
        private (GameObject, PhysicsWorld, PhysicsBody) Instantiate(GameObject prefab, Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            GameObject instance = _diContainer.InstantiatePrefab(prefab);
            instance.ToMoveToActiveScene();

            instance.transform.position = position;
            instance.transform.up = direction;

            PhysicsBody physicsBody = new PhysicsBody()
            {
                Velocity = direction * force,
                SpeedLimit = speedLimit,
                Direction = direction,
                Position = position,
                Drag = drag,
                Mass = _asteroidData.Mass,
            };

            PhysicsWorld physicsWorld = Resolve<PhysicsWorld>();
            physicsWorld.Add(physicsBody);
            instance.GetComponent<TransformView>().Init(physicsBody);
            return (instance, physicsWorld, physicsBody);
        }
        private T Resolve<T>() =>
            _diContainer.Resolve<T>();
    }
}