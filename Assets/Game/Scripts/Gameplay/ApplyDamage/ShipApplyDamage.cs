using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using System;
using UnityEngine;

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
        public void TakeDamage(float damage, HitType hitType)
        {
            if (_dead)
                return;

            if (_shipProgress.IsInvulnerable)
                return;

            int damageAmount = Mathf.CeilToInt(damage);
            if (damageAmount <= 0)
                return;

            _shipProgress.Health.ChangeHealth(_shipProgress.Health.CurrentHealth - damageAmount);
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