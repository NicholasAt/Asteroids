using Assets.Scripts.Data.Configs.Bullet;
using Assets.Scripts.Gameplay.Bullets;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
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
        private readonly ITickService _tickService;

        public BulletFactory(DiContainer diContainer, PhysicsWorld physicsWorld, BulletData bulletData, ITickService tickService)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _bulletData = bulletData;
            _tickService = tickService;
        }

        public ILaunched CreateBullet()
        {
            PhysicsBody body = new PhysicsBody()
            {
                Layer = CollisionLayer.Bullet,
                CollidesWith = CollisionLayer.Enemy,

                Mass = _bulletData.BulletMass,
                Radius = _bulletData.BulleRadius,
                Drag = 0,
            };
            GameObject instance = _diContainer.InstantiatePrefab(_bulletData.BulletPrefab);
            instance.ToMoveToActiveScene();

            Bullet bullet = new Bullet(_physicsWorld, body, _tickService, _bulletData.LifeTime);

            instance.GetComponent<BulletView>().Init(body, bullet);
            instance.GetComponent<TransformView>().Init(body);
            return bullet;
        }
    }
}