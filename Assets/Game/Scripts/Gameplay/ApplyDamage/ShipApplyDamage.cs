using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using System;

namespace Assets.Game.Scripts.Gameplay.ApplyDamage
{
    public class ShipApplyDamage : IApplyDamage
    {
        private readonly ShipProgress _shipProgress;
        private readonly float _invulnerabilitySeconds;

        public event Action OnDie;
        private bool _dead;
        public ShipApplyDamage(ShipProgress shipProgress, float invulnerabilitySeconds)
        {
            _shipProgress = shipProgress;
            _invulnerabilitySeconds = invulnerabilitySeconds;
        }
        public void TakeDamage(float damage, bool isSplit)
        {
            if (_dead)
                return;

            if (_shipProgress.IsInvulnerable)
                return;

            _shipProgress.Health.ChangeHealth(_shipProgress.Health.CurrentHealth - 1);
            if (_shipProgress.Health.CurrentHealth < 1)
            {
                _shipProgress.SetSpeed(0);
                _dead = true;
                OnDie?.Invoke();
            }
            else
                _shipProgress.SetInvulnerableLeft(_invulnerabilitySeconds);
        }
    }
}