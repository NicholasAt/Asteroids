using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Enemy;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class EnemyFactory
    {
        private readonly DiContainer _diContainer;
        private readonly AsteroidData _asteroidData;

        public EnemyFactory(DiContainer diContainer, AsteroidData asteroidData)
        {
            _diContainer = diContainer;
            _asteroidData = asteroidData;
        }

        public void CreateAsteroid(Vector2 position, Vector2 direction, float force, float speedLimit)
        {
            GameObject instance = _diContainer.InstantiatePrefab(_asteroidData.AsteroidPrefab);
            instance.ToMoveToActiveScene();

            instance.transform.position = position;
            instance.transform.up = direction;

            PhysicsBody physicsBody = new PhysicsBody()
            {
                Velocity = direction*force,
                SpeedLimit = speedLimit,
                Direction = direction,
                Position = position,
                Drag = 0,
                Mass = _asteroidData.Mass,
            };

            PhysicsWorld physicsWorld = _diContainer.Resolve<PhysicsWorld>();
            physicsWorld.Add(physicsBody);

            Asteroid asteroid = new Asteroid(physicsWorld, physicsBody);
            instance.GetComponent<AsteroidView>().Init(asteroid);
            instance.GetComponent<TransformView>().Init(physicsBody);
        }
    }
}