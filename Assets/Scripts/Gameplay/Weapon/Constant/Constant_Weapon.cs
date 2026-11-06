using Assets.Scripts.Data.Configs.Settings;
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
        private readonly LaserSettings _config;
        private readonly Constant_WeaponModel _model;
        private readonly Constant_WeaponEffect _weaponEffect;
        private readonly PhysicsBody _body;

        private bool _attackToggle;
        private List<PhysicsBody> _currentTargets = new();

        public Constant_Weapon(LaserSettings config, Constant_WeaponModel model, Constant_WeaponEffect weaponEffect, PhysicsBody body)
        {
            _config = config;
            _model = model;
            _weaponEffect = weaponEffect;
            _body = body;
            model.SetCharges(config.MaxCharges);
        }

        public override void Update(bool isPress, float deltaTime)
        {
            UpdateColldown(deltaTime);

            Vector2 direction = _body.Rotation.ToRotateToDirection();

            if (isPress && _attackToggle == false)
            {
                _attackToggle = true;

                if (_model.CurrentCharges > 0)
                {
                    _model.SetCharges(_model.CurrentCharges - 1);
                    _weaponEffect.Start(_config.RayWidth, _config.EffectDuration);
                    Fire(_body, direction, _config.RayLength, _config.RayWidth);
                }
            }
            else if (isPress == false && _attackToggle)
            {
                _attackToggle = false;
            }

            _weaponEffect.Update(_body.Position, _body.Position + (direction * _config.RayLength), deltaTime);
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
                    applyDamage.TakeDamage(_config.Damage, _config.IsSplit);
                }
            }
        }

        private void UpdateColldown(float deltaTime)
        {
            if (_model.CurrentCharges < _config.MaxCharges)
            {
                _model.SetCD(_model.CurrentCD + deltaTime);
                if (_model.CurrentCD >= _config.ChargeCooldown)
                {
                    _model.SetCharges(_model.CurrentCharges + 1);
                    _model.SetCD(_model.CurrentCD - _config.ChargeCooldown);
                }
            }
        }
    }
}