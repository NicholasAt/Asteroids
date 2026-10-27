using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Physics;

namespace Assets.Scripts.Gameplay.Move
{
    public class Move
    {
        private readonly PhysicsBody _physicsBody;
        private readonly float _speedRotate;
        private readonly float _forwardForce;

        public Move(PhysicsBody physicsBody, float speedRotate, float forwardForce)
        {
            _physicsBody = physicsBody;
            _speedRotate = speedRotate;
            _forwardForce = forwardForce;
        }

        public void Update(bool isThrust, float horizontalAxis)
        {
            _physicsBody.AngularForce = -horizontalAxis * _speedRotate;

            _physicsBody.Direction = _physicsBody.Rotation.ToRotateToDirection();
            _physicsBody.Force = isThrust ? _forwardForce : 0;
        }
    }
}