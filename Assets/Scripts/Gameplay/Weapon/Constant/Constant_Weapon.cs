using Assets.Scripts.Data.Weapon.Configs;
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
                _weaponEffect.Update();
                FireLaser(_body, 100, 1);
            }

            if (_attackToggle == false && isPress)
            {
                _attackToggle = true;
            }
            else if (_attackToggle && isPress == false)
            {
                _attackToggle = false;
                _weaponEffect.Stop();
            }
        }

        public void FireLaser(PhysicsBody ship, float length, float width)
        {
            Vector2 origin = ship.Position;
            Vector2 direction = Vector2.up;
            //Vector2 direction = new Vector2(Mathf.Cos(ship.Rotation), Mathf.Sin(ship.Rotation));
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