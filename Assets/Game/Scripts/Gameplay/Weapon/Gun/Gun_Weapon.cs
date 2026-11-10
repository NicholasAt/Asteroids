using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Weapon.Gun
{
    public class Gun_Weapon : BaseWeapon
    {
        private readonly GunSettings _config;
        private readonly float _cooldown;
        private readonly PhysicsBody _physicsBody;
        private readonly IBulletPool _bulletPool;
        private float _current;

        public Gun_Weapon(GunSettings config, PhysicsBody physicsBody, IBulletPool bulletPool)
        {
            _config = config;
            _physicsBody = physicsBody;
            _bulletPool = bulletPool;
            _cooldown = config.Cooldown;
            _current = _cooldown;
        }

        public override void Update(bool isPress, float deltaTime)
        {
            if (_current < _cooldown)
            {
                _current += deltaTime;
            }
            else if (isPress)
            {
                _current -= _cooldown;
                Vector2 direction = _physicsBody.Rotation.ToRotateToDirection();
                Vector2 from = _physicsBody.Position + direction * _config.ForwardSpawnPointOffset;
                _bulletPool.Get(from, _physicsBody.Rotation, false, _config.BulletForce, _config.BulletSpeedLimit);
            }
        }
    }
}