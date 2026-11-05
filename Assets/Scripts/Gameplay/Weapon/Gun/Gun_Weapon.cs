using Assets.Scripts.Data.Configs.Weapon.Configs;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Gun
{
    public class Gun_Weapon : BaseWeapon
    {
        private readonly Gun_WeaponConfig _config;
        private readonly PhysicsBody _physicsBody;
        private readonly IBulletPool _bulletPool;
        private float _current;

        public Gun_Weapon(Gun_WeaponConfig config, PhysicsBody physicsBody, IBulletPool bulletPool)
        {
            _config = config;
            _physicsBody = physicsBody;
            _bulletPool = bulletPool;
            _current = _config.CD;
        }

        public override void Update(bool isPress, float deltaTime)
        {
            if (_current < _config.CD)
            {
                _current += deltaTime;
            }
            else if (isPress)
            {
                _current -= _config.CD;
                Vector2 direction = _physicsBody.Rotation.ToRotateToDirection();
                Vector2 from = _physicsBody.Position + (direction * _config.ForwardSpawnPointOffset);
                _bulletPool.Get(from, _physicsBody.Rotation, _config.IsSplit, _config.Force, _config.BulletSpeedLimit);
            }
        }
    }
}