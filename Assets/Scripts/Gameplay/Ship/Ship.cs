using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Scripts.Gameplay.Physics;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : IWrapped, IApplyDamage, ICustomTick, IDisposable
    {
        public readonly PhysicsBody PhysicsBody;
        private readonly ShipProgress _shipProgress;
        private readonly IWorldBounds _worldBounds;
        private readonly IInput _input;
        private readonly ITickService _tickService;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly Move.Move _move;
        private readonly float _invulnerabilitySeconds;

        private IWeapon _weapon1;
        private IWeapon _weapon2;
        public Vector2 Position => PhysicsBody.Position;
        private bool _invulnerableToggle;

        public Ship(PhysicsWorld physicsWorld, float invulnerabilitySeconds, ShipData shipData, Move.Move move, PhysicsBody physicsBody, ShipProgress shipProgress, IWorldBounds worldBounds, IInput input, ITickService tickService)
        {
            PhysicsBody = physicsBody;
            _shipProgress = shipProgress;
            _physicsWorld = physicsWorld;
            _invulnerabilitySeconds = invulnerabilitySeconds;
            _shipData = shipData;
            _move = move;
            _worldBounds = worldBounds;
            _input = input;
            _tickService = tickService;
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
        }

        public void TakeDamage(float _)
        {
            if (_shipProgress.IsInvulnerable)
                return;

            _shipProgress.Health.ChangeHealth(_shipProgress.Health.CurrentHealth - 1);
            if (_shipProgress.Health.CurrentHealth < 1)
            {
                Debug.LogError("sdoh");
                Dispose();
            }
            else
                _shipProgress.SetInvulnerableLeft(_invulnerabilitySeconds);
        }

        public void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
            _shipProgress.SetPositionAndRotate(Position, PhysicsBody.Rotation);
        }

        public void Dispose()
        {
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
            _tickService.Remove(this);
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
                PhysicsBody.IsInvulnerable = true;
                _invulnerableToggle = true;
            }
            else if (_shipProgress.IsInvulnerable == false && _invulnerableToggle)
            {
                PhysicsBody.IsInvulnerable = false;
                _invulnerableToggle = false;
            }
        }
    }
}