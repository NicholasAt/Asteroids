using Assets.Scripts.Gameplay.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private PhysicsBody _physicsBody;

        public void Init(PhysicsBody physicsBody)
        {
            _physicsBody = physicsBody;
        }

    }
}