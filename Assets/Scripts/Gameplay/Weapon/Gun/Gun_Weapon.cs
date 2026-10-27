using Assets.Scripts.Data.Weapon.Configs;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Gun
{
    public class Gun_Weapon : BaseWeapon
    {
        private const float CD = 1;
        private readonly Gun_WeaponConfig _config;
        private readonly PhysicsBody _physicsBody;
        private readonly IBulletFactory _bulletFactory;
        private float _current;

        public Gun_Weapon(Gun_WeaponConfig config, PhysicsBody physicsBody, IBulletFactory bulletFactory)
        {
            _config = config;
            _physicsBody = physicsBody;
            _bulletFactory = bulletFactory;
        }

        public override void Update(bool isPress, float deltaTime)
        {
            if (_current < CD)
            {
                _current += deltaTime;
            }
            else if (isPress)
            {
                _current -= CD;
                Vector2 from = _physicsBody.Position + (_physicsBody.Rotation.ToRotateToDirection() * _config.ForwardSpawnPointOffset);
                _bulletFactory.CreateBullet(from, _physicsBody.Rotation, _config.BulletSpeedLimit);
            }
        }
    }
}