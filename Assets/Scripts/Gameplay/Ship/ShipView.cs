using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class ShipView : MonoBehaviour
    {
        private float CD = 0.1f;
        [SerializeField] private SpriteRenderer _body;

        private IInput _inputService;
        private ShipProgress _shipProgress;
        private float _currentCD;

        [Inject]
        private void Construct(IInput inputService, IProgressService progressService)
        {
            _inputService = inputService;
            _shipProgress = progressService.ShipProgress;
        }

        private void Update()
        {
            UpdateEffect();
        }
        private void UpdateEffect()
        {
            if (_shipProgress.IsInvulnerable)
            {
                _currentCD += Time.deltaTime;
                if (_currentCD >= CD)
                {
                    _currentCD -= CD;
                    _body.enabled = !_body.enabled;
                }
            }
            else
                _body.enabled = true;
        }
    }
}