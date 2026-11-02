using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class ShipView : MonoBehaviour
    {
        private IInput _inputService;
        private Ship _ship;

        [Inject]
        private void Construct(IInput inputService)
        {
            _inputService = inputService;
        }
        public void Init(Ship ship)
        {
            _ship = ship;
        }

        private void Update()
        {
            _ship.Update(Time.deltaTime, _inputService.IsAttack1, _inputService.IsAttack2);
        }
        private void OnDestroy()
        {
            _ship.Destroy();
        }
    }
}