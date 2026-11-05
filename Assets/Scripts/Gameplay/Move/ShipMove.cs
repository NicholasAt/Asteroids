using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;

namespace Assets.Scripts.Gameplay.Move
{
    public class ShipMove
    {
        private readonly PhysicsBody _physicsBody;
        private readonly IInput _input;
        private readonly float _speedRotate;
        private readonly float _forwardForce;

        public ShipMove(PhysicsBody physicsBody, IInput input, float speedRotate, float forwardForce)
        {
            _physicsBody = physicsBody;
            _input = input;
            _speedRotate = speedRotate;
            _forwardForce = forwardForce;
        }

        public void Update(bool canMove)
        {
            bool isThrust = _input.MoveAxis.y > 0 && canMove;
            float horizontalAxis = canMove ? _input.MoveAxis.x : 0;
            _physicsBody.AngularForce = -horizontalAxis * _speedRotate;

            _physicsBody.Direction = _physicsBody.Rotation.ToRotateToDirection();
            _physicsBody.Force = isThrust ? _forwardForce : 0;
        }
    }
}