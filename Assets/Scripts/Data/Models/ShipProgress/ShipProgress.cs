using System;
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

        public ShipProgress(Health health)
        {
            Health = health;
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
            Health.Dispose();
        }
    }
}