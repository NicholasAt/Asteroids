using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Services.InputService;
using Assets.Scripts.Gameplay.Weapon;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : MonoBehaviour
    {
        private IInputService _inputService;
        private ShipData _shipData;
        private IWeapon _weapon1;
        private IWeapon _weapon2;

        [Inject]
        private void Construct(IInputService inputService, ShipData shipData)
        {
            _inputService = inputService;
            _shipData = shipData;
        }
        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
        }

        private void Update()
        {
            UpdateMove();

            _weapon1?.Update(_inputService.IsAttack1);
            _weapon2?.Update(_inputService.IsAttack2);
        }

        private void UpdateMove()
        {
            Vector2 moveAxis = Vector2.ClampMagnitude(_inputService.MoveAxis, 1);
            Vector2 direction = _shipData.Speed * Time.deltaTime * moveAxis;

            transform.position += new Vector3(direction.x, direction.y);
        }
    }
}