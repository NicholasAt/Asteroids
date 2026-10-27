using Assets.Scripts.Data.Weapon.Configs;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;

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
                PhysicsBody bullet = _bulletFactory.CreateBullet();
                bullet.Position = _physicsBody.Position;
                bullet.Rotation = _physicsBody.Rotation;
                bullet.Velocity = _physicsBody.Direction;
            }
        }
    }
}