using Assets.Scripts.Data.Configs.Weapon.Configs;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Constant
{
    public class Constant_Weapon : BaseWeapon
    {
        private readonly Constant_WeaponConfig _config;
        private readonly Constant_WeaponModel _model;
        private readonly Constant_WeaponEffect _weaponEffect;
        private readonly PhysicsBody _body;

        private bool _attackToggle;
        private List<PhysicsBody> _currentTargets = new();

        private bool CDDamage => _currentCDDamage > 0;
        private float _currentCDDamage;

        public Constant_Weapon(Constant_WeaponConfig config, Constant_WeaponModel model, Constant_WeaponEffect weaponEffect, PhysicsBody body)
        {
            _config = config;
            _model = model;
            _weaponEffect = weaponEffect;
            _body = body;
        }

        public override void Update(bool isPress, float deltaTime)
        {
            if (CDDamage)
                _currentCDDamage -= deltaTime;

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

            if (isPress)
            {
                Vector2 direction = _body.Rotation.ToRotateToDirection();

                if (CDDamage == false)
                {
                    Fire(_body, direction, _config.RayLength, _config.RayWidth);
                    _currentCDDamage += _config.DamageDelay;
                }
                _weaponEffect.Update(_body.Position, _body.Position + (direction * _config.RayLength));
            }
        }

        private void Fire(PhysicsBody root, Vector2 direction, float length, float width)
        {
            Vector2 origin = root.Position;
            Vector2 end = origin + direction * length;

            PhysicsWorld.RaycastAll(origin, end, ref _currentTargets, width);

            foreach (PhysicsBody body in _currentTargets)
            {
                if (body == root)
                    continue;

                if (body.Owner != null && body.Owner is IApplyDamage applyDamage)
                {
                    applyDamage.TakeDamage(_config.Damage);
                }
            }
        }
    }
}