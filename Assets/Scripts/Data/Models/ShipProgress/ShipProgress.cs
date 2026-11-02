using UnityEngine;

namespace Assets.Scripts.Data.Models.ShipProgress
{
    public class ShipProgress
    {
        public readonly Health Health;
        public bool IsInvulnerable => InvulnerableLeft > 0;
        public float InvulnerableLeft { get; private set; }
        public Vector2 Position { get; private set; }

        public ShipProgress(Health health)
        {
            Health = health;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
        }

        public void SetInvulnerableLeft(float invulnerableLeft)
        {
            InvulnerableLeft = invulnerableLeft;
        }
    }
}