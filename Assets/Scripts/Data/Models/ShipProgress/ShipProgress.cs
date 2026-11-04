using Assets.Scripts.Data.Models.Weapon;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Models.ShipProgress
{
    public class ShipProgress : IDisposable
    {
        public readonly Health Health;
        public bool IsInvulnerable => InvulnerableLeft > 0;
        public float InvulnerableLeft { get; private set; }
        public Vector2 Position { get; private set; }
        public float Rotate { get; private set; }
        public IReadOnlyList<IWeaponModel> Weapons { get; private set; }
        public event Action OnWeaponChanged;

        public ShipProgress(Health health)
        {
            Health = health;
        }
        public void SetWeapons(IReadOnlyList<IWeaponModel> baseWeapons)
        {
            Weapons = baseWeapons;
            OnWeaponChanged?.Invoke();
        }
        public void SetPositionAndRotate(Vector2 position, float rotate)
        {
            Position = position;
            Rotate = rotate;
        }

        public void SetInvulnerableLeft(float invulnerableLeft)
        {
            InvulnerableLeft = invulnerableLeft;
        }

        public void Dispose()
        {
            OnWeaponChanged = null;
            Health.Dispose();
        }
    }
}