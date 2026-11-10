using Assets.Game.Scripts.Data.Configs.Bullet;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Bullets;
using Assets.Game.Scripts.Gameplay.Common;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services.Factories
{
    public class BulletFactory : IBulletFactory
    {
        private readonly DiContainer _diContainer;
        private readonly PhysicsWorld _physicsWorld;
        private readonly BulletData _bulletData;
        private readonly BulletSettings _bulletSettings;
        private readonly ITickService _tickService;

        public BulletFactory(DiContainer diContainer, PhysicsWorld physicsWorld, BulletData bulletData, PlayerSettings playerSettings, ITickService tickService)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _bulletData = bulletData;
            _bulletSettings = playerSettings.Gun.Bullet;
            _tickService = tickService;
        }

        public ILaunched CreateBullet()
        {
            PhysicsBody body = new PhysicsBody()
            {
                Layer = CollisionLayer.Bullet,
                CollidesWith = CollisionLayer.Enemy,

                Mass = _bulletSettings.Mass,
                Radius = _bulletSettings.Radius,
                Drag = 0,
            };
            GameObject instance = _diContainer.InstantiatePrefab(_bulletData.BulletPrefab);
            instance.ToMoveToActiveScene();

            Bullet bullet = new Bullet(_physicsWorld, body, _tickService, _bulletSettings.LifeTime, _bulletSettings.Damage);

            instance.GetComponent<BulletView>().Init(bullet);
            instance.GetComponent<TransformView>().Init(body);
            return bullet;
        }
    }
}