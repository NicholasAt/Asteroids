using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Services.InputService;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : MonoBehaviour
    {
        private IInputService _inputService;
        private ShipData _shipData;

        [Inject]
        private void Construct(IInputService inputService, ShipData shipData)
        {
            _inputService = inputService;
            _shipData = shipData;
        }
        private void Update()
        {
            Vector2 moveAxis = Vector2.ClampMagnitude(_inputService.MoveAxis, 1);
            Vector2 direction = _shipData.Speed * Time.deltaTime * moveAxis;

            transform.position += new Vector3(direction.x, direction.y);
        }
    }
}