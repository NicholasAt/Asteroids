using Assets.Scripts.Data.Weapon.Configs;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Constant
{
    public class Constant_Weapon : BaseWeapon
    {
        private readonly Constant_WeaponConfig _config;
        private readonly Constant_WeaponEffect _weaponEffect;
        private readonly PhysicsBody _body;

        private bool _attackToggle;
        private List<PhysicsBody> _currentTargets = new();

        public Constant_Weapon(Constant_WeaponConfig config, Constant_WeaponEffect weaponEffect, PhysicsBody body)
        {
            _config = config;
            _weaponEffect = weaponEffect;
            _body = body;
        }

        public override void Update(bool isPress, float deltaTime)
        {
            if (isPress)
            {
                Vector2 direction = _body.Rotation.ToRotateToDirection();
                Fire(_body, direction, 100, _config.RayWidth);
                _weaponEffect.Update(_body.Position, direction * 100);
            }

            if (_attackToggle == false && isPress)
            {
                _attackToggle = true;
                _weaponEffect.Start(_config.RayWidth);
            }
            else if (_attackToggle && isPress == false)
            {
                _attackToggle = false;
                _weaponEffect.Stop();
            }
        }

        private void Fire(PhysicsBody ship, Vector2 direction, float length, float width)
        {
            Vector2 origin = ship.Position;
            Vector2 end = origin + direction * length;

            PhysicsWorld.RaycastAll(origin, end, ref _currentTargets, width);

            foreach (PhysicsBody body in _currentTargets)
            {
                if (body == ship)
                    continue;

                Debug.LogError("contact");
            }
        }
    }
}