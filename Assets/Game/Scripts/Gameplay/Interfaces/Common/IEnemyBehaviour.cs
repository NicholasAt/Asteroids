using Assets.Game.Scripts.Data.Configs.Enemy;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface IEnemyBehaviour : IApplyDamage, IWrapped
    {
        event Action OnShow;
        EnemyType EnemyType { get; }
        void Show(Vector2 position, Vector2 direction);
        void Hide();
    }
}