using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Asteroid
    {
        private readonly PhysicsWorld _physicsWorld;
        private readonly PhysicsBody _physicsBody;

        public Asteroid(PhysicsWorld physicsWorld, PhysicsBody physicsBody)
        {
            _physicsWorld = physicsWorld;
            _physicsBody = physicsBody;
        }
       
        public void Destroy()
        {
            _physicsWorld.Remove(_physicsBody);
        }
    }
}