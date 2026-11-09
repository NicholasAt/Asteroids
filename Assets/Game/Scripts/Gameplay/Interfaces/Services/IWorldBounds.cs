using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services
{
    public interface IWorldBounds
    {
        void AddInMap(IWrapped wrappers);
        void AddOutMap(IWrapped wrappers);
        void CalculateRandomPositions(int randomAngle, int count, Action<Vector2, Vector2> onCalculate);
        void Remove(IWrapped wrappers);
    }
}