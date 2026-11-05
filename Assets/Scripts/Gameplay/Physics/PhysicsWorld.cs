using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Physics
{
    public class PhysicsWorld
    {
        private const float MinImpulseMagnitude = 15;

        private readonly static List<PhysicsBody> _physics = new(1000);
        private readonly HashSet<PhysicsBody> _candidates = new();

        public void Update(float deltaTime)
        {
            foreach (PhysicsBody target in _physics)
            {
                target.Simulate(deltaTime);
            }

            for (int i = 0; i < _physics.Count; i++)
            {
                for (int j = i + 1; j < _physics.Count; j++)
                {
                    PhysicsBody a = _physics[i];
                    PhysicsBody b = _physics[j];

                    if (CheckCollision(a, b))
                    {
                        a.OnCollision?.Invoke(b.Owner);
                        b.OnCollision?.Invoke(a.Owner);
                        ResolveCollision(a, b);
                    }
                }
            }
            UpdateRemove();
        }

        public void Add(PhysicsBody physicsBody)
        {
            _physics.Add(physicsBody);
        }
        public void Remove(PhysicsBody physicsBody)
        {
            _candidates.Add(physicsBody);
        }
        public static void RaycastAll(Vector2 from, Vector2 to, ref List<PhysicsBody> bodies, float width)
        {
            bodies.Clear();
            Vector2 dir = to - from;
            float length = dir.magnitude;

            dir /= length;

            foreach (PhysicsBody body in _physics)
            {
                Vector2 toCenter = body.Position - from;
                float projection = Vector2.Dot(toCenter, dir);
                projection = Mathf.Clamp(projection, 0f, length);

                Vector2 closest = from + dir * projection;
                float dist = Vector2.Distance(closest, body.Position);

                if (dist <= body.Radius + width * 0.5f)
                {
                    bodies.Add(body);
                }
            }
        }

        private void UpdateRemove()
        {
            if (_candidates.Count > 0)
            {
                foreach (PhysicsBody item in _candidates)
                {
                    _physics.Remove(item);
                }
                _candidates.Clear();
            }
        }

        private bool CheckCollision(PhysicsBody a, PhysicsBody b)
        {
            if ((a.CollidesWith & b.Layer) == 0 || (b.CollidesWith & a.Layer) == 0)
                return false;

            if (a.IsInvulnerable || b.IsInvulnerable)
                return false;

            float distanceSquared = (a.Position - b.Position).sqrMagnitude;
            float radiusSum = a.Radius + b.Radius;

            return distanceSquared <= radiusSum * radiusSum;
        }

        private void ResolveCollision(PhysicsBody a, PhysicsBody b, float restitution = 0.8f)
        {
            Vector2 difference = b.Position - a.Position;
            float distance = difference.magnitude;

            if (distance >= a.Radius + b.Radius)
                return;

            Vector2 normal = distance > 0.0001f ? difference / distance : Vector2.right;

            Vector2 relativeVelocity = b.Velocity - a.Velocity;

            float velocityAlongNormal = Vector2.Dot(relativeVelocity, normal);

            if (velocityAlongNormal < 0)
            {
                float inverseMassA = 1f / a.Mass;
                float inverseMassB = 1f / b.Mass;

                float impulseMagnitude = -(1f + restitution) * velocityAlongNormal / (inverseMassA + inverseMassB);
                impulseMagnitude = Mathf.Max(impulseMagnitude, MinImpulseMagnitude);
                Vector2 impulse = impulseMagnitude * normal;

                a.Velocity -= impulse * inverseMassA;
                b.Velocity += impulse * inverseMassB;
            }

            float penetration = a.Radius + b.Radius - distance;

            float totalInverseMass = 1f / a.Mass + 1f / b.Mass;
            Vector2 correction = normal * (penetration / totalInverseMass);

            a.Position -= correction / a.Mass;
            b.Position += correction / b.Mass;
        }
    }
}