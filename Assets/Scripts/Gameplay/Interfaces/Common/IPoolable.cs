using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IPoolable
    {
        event Action OnShow;
        event Action OnHide;
        void Show(Vector2 position,Vector2 direction);
        void Hide();
    }
}