using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Scripts.Gameplay.Move;
using Assets.Scripts.Gameplay.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : IWrapped, IApplyDamage, ICustomTick, IDisposable, IWeaponKeeper
    {
        public readonly PhysicsBody PhysicsBody;
        private readonly ShipProgress _shipProgress;
        private readonly IApplyDamage _applyDamage;
        private readonly IInput _input;
        private readonly ShipData _shipData;
        private readonly ShipMove _move;

        private IWeapon _weapon1;
        private IWeapon _weapon2;
        public Vector2 Position => PhysicsBody.Position;
        private bool _invulnerableToggle;

        public event Action OnDie;

        public Ship(ShipData shipData, ShipMove move, PhysicsBody physicsBody, ShipProgress shipProgress, IApplyDamage applyDamage, IInput input)
        {
            PhysicsBody = physicsBody;
            _shipProgress = shipProgress;
            _shipData = shipData;
            _move = move;
            _input = input;
            _applyDamage = applyDamage;
            _applyDamage.OnDie += Dispose;
        }

        public void SetWeapon(IReadOnlyList<IWeapon> weapons)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                IWeapon weapon = weapons[i];
                if (i == 0)
                    _weapon1 = weapon;
                else if (i == 1)
                    _weapon2 = weapon;
                else
                    Debug.LogError("too weapons");
            }
        }

        public void Tick(float deltaTime)
        {
            bool canMove = !_shipProgress.IsInvulnerable;
            _move.Update(canMove);

            _weapon1?.Update(_input.IsAttack1 && canMove, deltaTime);
            _weapon2?.Update(_input.IsAttack2 && canMove, deltaTime);

            _shipProgress.SetPositionAndRotate(Position, PhysicsBody.Rotation);
            UpdateInvulnerability(deltaTime);
            _shipProgress.SetSpeed(PhysicsBody.Velocity.magnitude);
        }

        public void TakeDamage(float damage, bool isSplit)
        {
            _applyDamage.TakeDamage(damage, isSplit);
        }

        public void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
            _shipProgress.SetPositionAndRotate(Position, PhysicsBody.Rotation);
        }

        public void Dispose()
        {
            _applyDamage.OnDie -= Dispose;
        }

        private void UpdateInvulnerability(float deltaTime)
        {
            if (_shipProgress.IsInvulnerable)
            {
                float newValue = Mathf.Max(_shipProgress.InvulnerableLeft - deltaTime, 0);
                _shipProgress.SetInvulnerableLeft(newValue);
            }

            if (_shipProgress.IsInvulnerable && _invulnerableToggle == false)
            {
                _invulnerableToggle = true;
            }
            else if (_shipProgress.IsInvulnerable == false && _invulnerableToggle)
            {
                _invulnerableToggle = false;
            }
            PhysicsBody.IsInvulnerable = _shipProgress.IsInvulnerable;
        }
    }
}