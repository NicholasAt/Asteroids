using Assets.Scripts.Data.Configs.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services
{
    public class WorldBounds : IWorldBounds, ICustomTick
    {
        private readonly List<IWrapped> _outMap = new();
        private readonly List<IWrapped> _inMap = new();
        private readonly WorldBoundsData _worldBoundsData;
        private Vector2 _inMapHalfSize, _outMapHalfSize;

        public WorldBounds(WorldBoundsData worldBoundsData)
        {
            _worldBoundsData = worldBoundsData;
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

        public void Tick(float _)
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

        public void CalculateRandomPositions(int randomAngle, int count, Action<Vector2, Vector2> onCalculate)
        {
            int halfRandomAngle = randomAngle / 2;
            Vector2 halfMap = _worldBoundsData.OutMapSize / 2;

            for (int i = 0; i < count; i++)
            {
                bool upDown = UnityEngine.Random.Range(0, 2) == 1;
                bool forward = UnityEngine.Random.Range(0, 2) == 1;

                Vector2 pos;
                Vector2 rawDirection;

                if (upDown)
                {
                    pos.y = forward ? halfMap.y : -halfMap.y;
                    pos.x = UnityEngine.Random.Range(-halfMap.x, halfMap.x);
                    if (forward)
                    {
                        pos.y = halfMap.y;
                        rawDirection = Vector2.down;
                    }
                    else
                    {
                        pos.y = -halfMap.y;
                        rawDirection = Vector2.up;
                    }
                }
                else
                {
                    pos.x = forward ? halfMap.x : -halfMap.x;
                    pos.y = UnityEngine.Random.Range(-halfMap.y, halfMap.y);
                    if (forward)
                    {
                        pos.x = halfMap.x;
                        rawDirection = Vector2.right;
                    }
                    else
                    {
                        pos.x = -halfMap.x;
                        rawDirection = Vector2.left;
                    }
                }
                float offset = Mathf.Atan2(rawDirection.x, rawDirection.y);
                float randomRad = UnityEngine.Random.Range(-halfRandomAngle, halfRandomAngle) * Mathf.Deg2Rad;
                randomRad += offset;

                Vector2 direction = new(-Mathf.Sin(randomRad), Mathf.Cos(randomRad));
                onCalculate?.Invoke(pos, direction);
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