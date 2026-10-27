using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Constant
{
    public class Constant_WeaponEffect
    {
        private readonly PhysicsBody _body;
        private readonly LineRenderer _lineRenderer;

        public Constant_WeaponEffect(PhysicsBody body, LineRenderer lineRenderer)
        {
            _body = body;
            _lineRenderer = lineRenderer;
        }
        public void Update()
        {
            //PhysicsWorld.Q(_body.Position, Vector2.up);
        }
        public void Stop()
        {

        }
    }
}