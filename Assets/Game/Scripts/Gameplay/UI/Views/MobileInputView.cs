using Assets.Game.Scripts.Gameplay.UI.Common;
using Assets.Game.Scripts.Gameplay.UI.ViewModels;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.UI.Views
{
    public class MobileInputView : MonoBehaviour
    {
        [SerializeField] private HoldButton _attack1Button, _attack2Button;
        [SerializeField] private HoldButton _moveLeftButton, _moveRightButton;
        [SerializeField] private HoldButton _thrustButton;

        private MobileInputViewModel _mobileInputViewModel;

        [Inject]
        private void Construct(MobileInputViewModel mobileInputViewModel)
        {
            _mobileInputViewModel = mobileInputViewModel;
        }

        private void Update()
        {
            UpdateInput();
        }

        private void OnDisable()
        {
            ResetInput();
        }

        private void UpdateInput()
        {
            float horizontal = _moveLeftButton.IsPress ? -1 : _moveRightButton.IsPress ? 1 : 0;
            Vector2 axis = new Vector2(horizontal, _thrustButton.IsPress ? 1 : 0);

            _mobileInputViewModel.UpdateMoveAxis(axis);
            _mobileInputViewModel.SetAttack1(_attack1Button.IsPress);
            _mobileInputViewModel.SetAttack2(_attack2Button.IsPress);
        }

        private void ResetInput()
        {
            _mobileInputViewModel.UpdateMoveAxis(Vector2.zero);
            _mobileInputViewModel.SetAttack1(false);
            _mobileInputViewModel.SetAttack2(false);
        }
    }
}