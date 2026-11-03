using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Scripts.Gameplay.Physics;
using System;
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
        private readonly PlayerData _playerData;
        private readonly Move.Move _move;

        private IWeapon _weapon1;
        private IWeapon _weapon2;
        public Vector2 Position => PhysicsBody.Position;
        private bool _invulnerableToggle;

        public Ship(PhysicsWorld physicsWorld, PlayerData playerData, ShipData shipData, Move.Move move, PhysicsBody physicsBody, ShipProgress shipProgress, IWorldBounds worldBounds, IInput input, ITickService tickService)
        {
            PhysicsBody = physicsBody;
            _shipProgress = shipProgress;
            _physicsWorld = physicsWorld;
            _playerData = playerData;
            _shipData = shipData;
            _move = move;
            _worldBounds = worldBounds;
            _input = input;
            _tickService = tickService;
        }

        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
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
                _shipProgress.SetInvulnerableLeft(_playerData.InvulnerabilitySeconds);
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