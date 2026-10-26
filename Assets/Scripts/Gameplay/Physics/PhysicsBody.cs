using UnityEngine;

namespace Assets.Scripts.Gameplay.Physics
{
    public class PhysicsBody
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public float Rotation;
        public float AngularVelocity;
        public float Radius = 1;
        public float Mass = 1f;
        public float Drag = 1;
        public bool IsInvulnerable;
        public float DeltaTime;

        public Vector2 Direction;
        public float Force;

        public void Simulate()
        {
            ApplyForce();
            ApplyDrag();
            Integrate();
        }

        private void ApplyForce()
        {
            Velocity += DeltaTime * Force * Direction;
        }
        private void ApplyDrag()
        {
            Velocity -= DeltaTime * Drag * Velocity;
        }

        private void Integrate()
        {
            Position += Velocity * DeltaTime;
            Rotation += AngularVelocity * DeltaTime;
        }
    }
}