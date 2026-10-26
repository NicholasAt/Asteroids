using Assets.Scripts.Gameplay.Services.InputService;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class ShipView : MonoBehaviour
    {
        private IInputService _inputService;
        private Ship _ship;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }
        public void Init(Ship ship)
        {
            _ship = ship;
        }

        private void Update()
        {
            _ship.Update(_inputService.MoveAxis, _inputService.IsAttack1, _inputService.IsAttack2);
        }

        private void LateUpdate()
        {
            transform.position = _ship.GetPosition();
        }

        private void OnDestroy()
        {
            _ship.Destroy();
        }
    }
}