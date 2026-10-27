using Assets.Scripts.Data.Bullet;
using Assets.Scripts.Gameplay.Bullet;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class BulletFactory : IBulletFactory
    {
        private readonly DiContainer _diContainer;
        private readonly PhysicsWorld _physicsWorld;
        private readonly BulletData _bulletData;

        public BulletFactory(DiContainer diContainer, PhysicsWorld physicsWorld, BulletData bulletData)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _bulletData = bulletData;
        }

        public PhysicsBody CreateBullet(Vector2 position, float rotate, float speedLimit)
        {
            Vector2 direction = rotate.ToRotateToDirection();
            PhysicsBody body = new PhysicsBody()
            {
                Mass = _bulletData.BulletMass,
                Radius = _bulletData.BulleRadius,
                Drag = 0,
                SpeedLimit = speedLimit,
                Position = position,
                Rotation = rotate,
                Direction = direction,
                Velocity = direction * speedLimit,
            };
            _physicsWorld.Add(body);
            GameObject instance = _diContainer.InstantiatePrefab(_bulletData.BulletPrefab);
            instance.ToMoveToActiveScene();
            instance.GetComponent<BulletView>().Init(body);
            instance.GetComponent<TransformView>().Init(body);
            return body;
        }
    }
}