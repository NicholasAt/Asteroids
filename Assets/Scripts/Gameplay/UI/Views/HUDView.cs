using Assets.Scripts.Core.Common;
using Assets.Scripts.Gameplay.UI.ViewModels;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.UI.Views
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText, _rotationText;
        [SerializeField] private TMP_Text _laserCooldownText, _laserChargesText;
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _currentSpeed;

        private readonly List<Action> _unsubscribe = new List<Action>();
        private HUDViewModel _vm;

        [Inject]
        private void Construct(HUDViewModel hUDViewModel)
        {
            _vm = hUDViewModel;
        }

        private void Start()
        {
            Bind(_vm.Score, v => _scoreText.text = $"Score: {v}");
            Bind(_vm.CurrentHealth, v => _healthText.text = $"Health {_vm.CurrentHealth.Value}");

            Bind(_vm.LaserCharges, v => _laserChargesText.text = $"Laser charges: {v}/{_vm.MaxLaserCharges.Value}");
            Bind(_vm.LaserCooldown, v => _laserCooldownText.text = _vm.LaserCharges.Value < _vm.MaxLaserCharges.Value ? $"Laser cooldown: {v:F1}" : "Laser cooldown: ready");

            Bind(_vm.Position, v => _positionText.text = $"Position: {v.x:F0}, {v.y:F0}");
            Bind(_vm.Rotate, v => _rotationText.text = $"Angle: {v:F0}");
            Bind(_vm.Speed, v => _currentSpeed.text = $"Speed: {v:F1}");
        }

        private void OnDestroy()
        {
            foreach (Action u in _unsubscribe)
                u();
            _unsubscribe.Clear();
        }

        private void Bind<T>(ReactiveProperty<T> property, Action<T> apply)
        {
            apply(property.Value);
            property.Changed += apply;
            _unsubscribe.Add(() => property.Changed -= apply);
        }
    }
}