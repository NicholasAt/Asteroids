using Assets.Scripts.Core.Common;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.UI
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText, _rotationText;
        [SerializeField] private TMP_Text _laserCooldownText, _laserChargesText;
        [SerializeField] private TMP_Text _healthText;

        private readonly List<Action> _unsubscribe = new List<Action>();
        private HUDViewModel _vm;

        [Inject]
        private void Construct(HUDViewModel hUDViewModel)
        {
            _vm = hUDViewModel;
        }

        private void Start()
        {
            Bind(_vm.MaxHealth, v => RefreshHealth(_vm.CurrentHealth.Value, _vm.MaxHealth.Value));
            Bind(_vm.CurrentHealth, v => RefreshHealth(_vm.CurrentHealth.Value, _vm.MaxHealth.Value));

            Bind(_vm.LaserCharges, v => _laserChargesText.text = v.ToString());
            Bind(_vm.LaserCooldown, v => _laserCooldownText.text = $"{v:F1}");

            Bind(_vm.Position, v => _positionText.text = $"X: {v.x:F1} Y: {v.y:F1}");
            Bind(_vm.Rotate, v => _rotationText.text = $"Angle: {v:F0}");
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

        private void RefreshHealth(int current, int max)
        {
            _healthText.text = $"Health: {current}/{max}";
        }
    }
}