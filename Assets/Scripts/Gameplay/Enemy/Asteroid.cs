using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Asteroid
    {
        private readonly IPhysicsWorld _physicsWorld;
        private readonly PhysicsBody _physicsBody;

        public Asteroid(IPhysicsWorld physicsWorld, PhysicsBody physicsBody)
        {
            _physicsWorld = physicsWorld;
            _physicsBody = physicsBody;
        }
       
        public Vector2 GetPosition()
        {
            return _physicsBody.Position;
        }
        public void Destroy()
        {
            _physicsWorld.Remove(_physicsBody);
        }
    }
}