using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Enemy.Fragments;
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

        public EnemyFactory(DiContainer diContainer, AsteroidData asteroidData, FragmentsData fragmentsData)
        {
            _diContainer = diContainer;
            _asteroidData = asteroidData;
            _fragmentsData = fragmentsData;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float force, float speedLimit,float drag)
        {
            (GameObject instance, PhysicsWorld physicsWorld, PhysicsBody physicsBody) = Instantiate(_asteroidData.AsteroidPrefab, position, direction, force, speedLimit, drag);

            Asteroid asteroid = new Asteroid(physicsWorld, physicsBody, Resolve<SignalBus>());
            asteroid.Init();

            instance.GetComponent<AsteroidView>().Init(asteroid);
        }

        public void CreateFragment(Vector2 position, Vector2 direction, float force, float speedLimit, float drag)
        {
            (GameObject instance, PhysicsWorld physicsWorld, PhysicsBody physicsBody) = Instantiate(_fragmentsData.FragmentPrefab, position, direction, force, speedLimit, drag);
        }

        private (GameObject, PhysicsWorld, PhysicsBody) Instantiate(GameObject prefab, Vector2 position, Vector2 direction, float force, float speedLimit,float drag)
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

            PhysicsWorld physicsWorld = _diContainer.Resolve<PhysicsWorld>();
            physicsWorld.Add(physicsBody);
            instance.GetComponent<TransformView>().Init(physicsBody);
            return (instance, physicsWorld, physicsBody);
        }
        private T Resolve<T>() =>
            _diContainer.Resolve<T>();
    }
}