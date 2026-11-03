using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.UI
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _positionText, _rotationText;
        [SerializeField] private TMP_Text _healthText;
        private HUDViewModel _vm;

        [Inject]
        private void Construct(HUDViewModel hUDViewModel)
        {
            _vm = hUDViewModel;
        }
        private void Start()
        {
            _vm.OnHealthChange += RefreshHealth;
            RefreshHealth();
        }

        private void OnDestroy()
        {
            _vm.OnHealthChange -= RefreshHealth;
        }

        private void LateUpdate()
        {
            RefreshPosition();
        }

        private void RefreshPosition()
        {
            _positionText.text = _vm.PositionText;
            _rotationText.text = _vm.RotateText;
        }
        private void RefreshHealth()
        {
            _healthText.text = $"Health: {_vm.CurrentHealth}/{_vm.MaxHealth}";
        }
    }
}