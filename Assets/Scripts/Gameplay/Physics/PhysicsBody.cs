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

        private Vector2 _direction;
        private float _force;

        public void Simulate()
        {
            AddForce();
            ApplyDrag();
            Integrate();
        }

        public void SetForce(Vector2 direction, float force)
        {
            _direction = direction;
            _force = force;
        }

        private void AddForce()
        {
            Velocity += DeltaTime * _force * _direction;
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