using Assets.Scripts.Gameplay.UI.Common;
using Assets.Scripts.Gameplay.UI.ViewModels;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.UI.Views
{
    public class MobileInputView : MonoBehaviour
    {
        [SerializeField] private HoldButton _attack1Button, _attack2Button;
        [SerializeField] private HoldButton _moveLeftButton, _moveRightBuuton;
        [SerializeField] private HoldButton _thrustButton;

        private MobileInputViewModel _vm;
        private CancellationToken _ct;

        [Inject]
        private void Construct(MobileInputViewModel vm)
        {
            _vm = vm;
        }

        private void Start()
        {
            _ct = this.GetCancellationTokenOnDestroy();
            UpdateInput(_ct).Forget();
        }
        private async UniTaskVoid UpdateInput(CancellationToken ct)
        {
            try
            {
                while (_ct.IsCancellationRequested == false)
                {
                    await UniTask.Yield(PlayerLoopTiming.PreUpdate, ct);

                    float horizontal = _moveLeftButton.IsPress ? -1 : _moveRightBuuton.IsPress ? 1 : 0;
                    Vector2 axis = new Vector2(horizontal, _thrustButton.IsPress ? 1 : 0);

                    _vm.UpdateMoveAxis(axis);
                    _vm.SetAttack1(_attack1Button.IsPress);
                    _vm.SetAttack2(_attack2Button.IsPress);
                }
            }
            catch (OperationCanceledException)
            {
                //ignore
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}