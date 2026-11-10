using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface ILaunched
    {
        event Action OnShow;
        event Action OnHide;
        void Launch(Vector2 position, float rotate, float force, float speedLimit);
        void Hide();
    }
}