using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Physics
{
    public class PhysicsBody
    {
        public Action<object> OnCollision { get; set; }
        public object Owner;
        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Direction;
        public float Force;

        public CollisionLayer Layer;
        public CollisionLayer CollidesWith;

        public float Rotation;
        public float AngularForce;
        public float AngularVelocity;
        public float Radius = 1;
        public float Mass = 1f;
        public float Drag = 1;
        public float AngularDrag = 1;

        public bool IsInvulnerable;
        public float SpeedLimit = 100;
        private float _deltaTime;
        public void Simulate(float deltaTime)
        {
            _deltaTime = deltaTime;
            ApplyThrust();
            ApplyDrag();
            Integrate();
        }

        private void ApplyThrust()
        {
            Velocity += _deltaTime * Force * Direction;
            Velocity = Vector2.ClampMagnitude(Velocity, SpeedLimit);

            AngularVelocity += _deltaTime * AngularForce;
        }
        private void ApplyDrag()
        {
            Velocity -= _deltaTime * Drag * Velocity;
            AngularVelocity -= AngularVelocity * AngularDrag * _deltaTime;
        }
        private void Integrate()
        {
            Position += Velocity * _deltaTime;
            Rotation += AngularVelocity * _deltaTime;
        }
    }
}