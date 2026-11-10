using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Weapon.Constant
{
    public class LaserWeaponEffect
    {
        private readonly LineRenderer _lineRenderer;
        private float _width;
        private float _duration;

        private bool _isStart;
        private float _currentDuration;

        public LaserWeaponEffect(LineRenderer lineRenderer)
        {
            _lineRenderer = lineRenderer;
        }

        public void Start(float width, float duration)
        {
            _isStart = true;
            _width = width;
            _duration = duration;
            _lineRenderer.enabled = true;
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _width * 0.5f;
        }

        public void Update(Vector2 from, Vector2 to, float deltaTime)
        {
            if (_isStart)
            {
                _isStart = false;
                _currentDuration = _duration;
            }

            if (_currentDuration > 0.0f)
            {
                _currentDuration -= deltaTime;
                _lineRenderer.SetPosition(0, from);
                _lineRenderer.SetPosition(1, to);
            }
            else
                _lineRenderer.enabled = false;
        }
    }
}