using Assets.Scripts.Data.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.WorldBounds
{
    public class WorldBounds : IWorldBounds, ITickable
    {
        private readonly List<IWrapped> _outMap = new();
        private readonly List<IWrapped> _inMap = new();
        private Vector2 _inMapHalfSize, _outMapHalfSize;

        public WorldBounds(WorldBoundsData worldBoundsData)
        {
            _inMapHalfSize = worldBoundsData.MapSize / 2;
            _outMapHalfSize = worldBoundsData.OutMapSize / 2;
        }

        public void AddOutMap(IWrapped wrapped)
        {
            _outMap.Add(wrapped);
        }

        public void AddInMap(IWrapped wrapped)
        {
            _inMap.Add(wrapped);
        }

        public void Remove(IWrapped wrapped)
        {
            if (_outMap.Remove(wrapped) == false)
                _inMap.Remove(wrapped);
        }

        public void Tick()
        {
            for (int i = _inMap.Count - 1; i >= 0; i--)
            {
                CheckAndWrap(_inMap[i], _inMapHalfSize.x, _inMapHalfSize.y);
            }
            for (int i = _outMap.Count - 1; i >= 0; i--)
            {
                CheckAndWrap(_outMap[i], _outMapHalfSize.x, _outMapHalfSize.y);
            }
        }

        private void CheckAndWrap(IWrapped wrapped, float halfWidth, float halfHeight)
        {
            Vector2 position = wrapped.Position;
            bool isWrap = false;

            if (position.x > halfWidth)
            {
                isWrap = true;
                position.x = -halfWidth;
            }

            else if (position.x < -halfWidth)
            {
                isWrap = true;
                position.x = halfWidth;
            }

            if (position.y > halfHeight)
            {
                isWrap = true;
                position.y = -halfHeight;
            }

            else if (position.y < -halfHeight)
            {
                isWrap = true;
                position.y = halfHeight;
            }
            if (isWrap)
                wrapped.Wrap(position);
        }
    }
}