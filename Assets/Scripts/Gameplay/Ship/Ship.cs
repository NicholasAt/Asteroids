using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Data.Player;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : IWrapped, IApplyDamage, ITickable, IDisposable
    {
        public readonly PhysicsBody PhysicsBody;
        private readonly ShipProgress _shipProgress;
        private readonly IWorldBounds _worldBounds;
        private readonly IInput _input;
        private readonly CancellationTokenSource _cts;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly PlayerData _playerData;
        private readonly Move.Move _move;
        private IWeapon _weapon1;
        private IWeapon _weapon2;
        public Vector2 Position => PhysicsBody.Position;

        public Ship(PhysicsWorld physicsWorld, PlayerData playerData, ShipData shipData, Move.Move move, PhysicsBody physicsBody, ShipProgress shipProgress, IWorldBounds worldBounds, IInput input)
        {
            PhysicsBody = physicsBody;
            _shipProgress = shipProgress;
            _physicsWorld = physicsWorld;
            _playerData = playerData;
            _shipData = shipData;
            _move = move;
            _worldBounds = worldBounds;
            _input = input;
            _cts = new CancellationTokenSource();
        }

        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
        }

        public void Tick()
        {
            bool canMove = !_shipProgress.IsInvulnerable;
            _move.Update(canMove);

            _weapon1?.Update(_input.IsAttack1 && canMove, Time.deltaTime);
            _weapon2?.Update(_input.IsAttack2 && canMove, Time.deltaTime);

            _shipProgress.SetPosition(Position);
        }

        public void TakeDamage(float _)
        {
            if (_shipProgress.IsInvulnerable)
                return;

            _shipProgress.Health.ChangeHealth(_shipProgress.Health.CurrentHealth - 1);
            if (_shipProgress.Health.CurrentHealth < 1)
                Debug.LogError("sdoh");
            PlayInvulnerability(_playerData.InvulnerabilitySeconds, _cts.Token).Forget();
        }

        public void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
            _shipProgress.SetPosition(Position);
        }

        public void Dispose()
        {
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
        }

        private async UniTask PlayInvulnerability(float duration, CancellationToken ct)
        {
            try
            {
                _shipProgress.SetInvulnerableLeft(duration);
                await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: ct);
                _shipProgress.SetInvulnerableLeft(0);
            }

            catch (OperationCanceledException)
            {
                //ignore
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}