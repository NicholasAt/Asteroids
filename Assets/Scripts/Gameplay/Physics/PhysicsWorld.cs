using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Physics
{
    public class PhysicsWorld : MonoBehaviour, IPhysicsWorld
    {
        private readonly HashSet<PhysicsBody> _physics = new(1000);

        public void Add(PhysicsBody physicsBody)
        {
            physicsBody.DeltaTime = Time.deltaTime;
            _physics.Add(physicsBody);
        }
        public void Remove(PhysicsBody physicsBody)
        {
            _physics.Remove(physicsBody);
        }

        private void Update()
        {
            foreach (PhysicsBody target in _physics)
            {
                target.Simulate();
            }
        }
    }
}