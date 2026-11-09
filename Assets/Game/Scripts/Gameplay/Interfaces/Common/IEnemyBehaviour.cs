using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface IEnemyBehaviour : IApplyDamage, IWrapped
    {
        event Action OnShow;
        void Show(Vector2 position, Vector2 direction);
        void Update(float deltaTime);
        void Hide();
    }
}