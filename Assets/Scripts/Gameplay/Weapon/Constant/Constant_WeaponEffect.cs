using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Weapon.Constant
{
    public class Constant_WeaponEffect
    {
        private readonly PhysicsBody _body;
        private readonly LineRenderer _lineRenderer;
        private float _width;

        public Constant_WeaponEffect(PhysicsBody body, LineRenderer lineRenderer)
        {
            _body = body;
            _lineRenderer = lineRenderer;
        }

        public void Start(float width)
        {
            _width = width;
            _lineRenderer.enabled = true;
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _width/2;
        }

        public void Update(Vector2 from,Vector2 to)
        {
            _lineRenderer.SetPosition(0, from);
            _lineRenderer.SetPosition(1, to);
        }

        public void Stop()
        {
            _lineRenderer.enabled = false;
        }
    }
}