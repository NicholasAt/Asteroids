using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Models.Weapon;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Weapon.Constant
{
    public class LaserWeapon : IWeapon
    {
        private readonly LaserSettings _config;
        private readonly ConstantWeaponModel _model;
        private readonly LaserWeaponEffect _weaponEffect;
        private readonly PhysicsBody _body;

        private bool _attackToggle;
        private List<PhysicsBody> _currentTargets = new();

        public LaserWeapon(LaserSettings config, ConstantWeaponModel model, LaserWeaponEffect weaponEffect, PhysicsBody body)
        {
            _config = config;
            _model = model;
            _weaponEffect = weaponEffect;
            _body = body;
            model.SetCharges(config.MaxCharges, config.MaxCharges);
            model.SetCooldownRemaining(config.ChargeCooldown);
        }

        public void Update(bool isPress, float deltaTime)
        {
            RestoreCharges(deltaTime);

            Vector2 rayStart = _body.Position;
            Vector2 rayEnd = rayStart + _body.Rotation.ToRotateToDirection() * _config.RayLength;

            UpdateAttack(isPress, rayStart, rayEnd);
            _weaponEffect.Update(rayStart, rayEnd, deltaTime);
        }

        private void RestoreCharges(float deltaTime)
        {
            if (_model.CurrentCharges >= _config.MaxCharges)
                return;

            _model.SetCooldownRemaining(_model.CooldownRemaining - deltaTime);
            if (_model.CooldownRemaining <= 0)
            {
                _model.SetCharges(_model.CurrentCharges + 1, _config.MaxCharges);
                _model.SetCooldownRemaining(_model.CooldownRemaining + _config.ChargeCooldown);
            }
        }

        private void UpdateAttack(bool isPress, Vector2 rayStart, Vector2 rayEnd)
        {
            if (isPress == false)
            {
                _attackToggle = false;
                return;
            }

            if (_attackToggle)
                return;

            _attackToggle = true;

            if (_model.CurrentCharges <= 0)
                return;

            _model.SetCharges(_model.CurrentCharges - 1, _config.MaxCharges);
            _weaponEffect.Start(_config.RayWidth, _config.EffectDuration);
            Fire(rayStart, rayEnd);
        }

        private void Fire(Vector2 rayStart, Vector2 rayEnd)
        {
            PhysicsWorld.RaycastAll(rayStart, rayEnd, ref _currentTargets, _config.RayWidth);

            foreach (PhysicsBody body in _currentTargets)
            {
                if (body == _body)
                    continue;

                if (body.Owner != null && body.Owner is IApplyDamage applyDamage)
                {
                    applyDamage.TakeDamage(_config.Damage, HitType.Laser);
                }
            }
        }
    }
}