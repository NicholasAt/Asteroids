using Assets.Scripts.Data.Bullet;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class BulletFactory: IBulletFactory
    {
        private readonly DiContainer _diContainer;
        private readonly BulletData _bulletData;

        public BulletFactory(DiContainer diContainer, BulletData bulletData)
        {
            _diContainer = diContainer;
            _bulletData = bulletData;
        }

        public PhysicsBody CreateBullet()
        {
            PhysicsBody body = new PhysicsBody()
            {
                Mass = 0.5f,
                Drag=0,
            };
            GameObject instance = _diContainer.InstantiatePrefab(_bulletData.BulletPrefab);
            instance.ToMoveToActiveScene();
            instance.GetComponent<TransformView>().Init(body);
            return body;
        }
    }
}