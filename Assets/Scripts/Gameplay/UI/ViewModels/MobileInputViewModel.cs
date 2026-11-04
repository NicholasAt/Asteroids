using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UI.ViewModels
{
    public class MobileInputViewModel : IInput
    {
        public Vector2 MoveAxis { get; private set; }

        public bool IsAttack1 { get; private set; }

        public bool IsAttack2 { get; private set; }

        public void SetAttack1(bool attack)
        {
            IsAttack1 = attack;
        }

        public void SetAttack2(bool attack)
        {
            IsAttack2 = attack;
        }

        public void UpdateMoveAxis(Vector2 moveAxis)
        {
            MoveAxis = moveAxis;
        }
    }
}