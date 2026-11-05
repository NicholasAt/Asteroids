using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Pool;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Pools
{
    public class BulletPool : IBulletPool
    {
        private readonly IBulletFactory _bulletFactory;
        private readonly ObjectsPool<ILaunched> _pool;

        public BulletPool(IBulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _pool = new(NewBullet);
        }

        public ILaunched Get(Vector2 position, float rotate, bool isSplit, float force, float speedLimit)
        {
            ILaunched bullet = _pool.Pop();
            bullet.Launch(position, rotate, isSplit, force, speedLimit);
            return bullet;
        }

        private ILaunched NewBullet()
        {
            ILaunched bullet = _bulletFactory.CreateBullet();
            bullet.OnHide += () => _pool.Return(bullet);
            return bullet;
        }
    }
}