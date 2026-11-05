using Assets.Scripts.Data.Configs.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services
{
    public class WorldFrame : IWorldFrame
    {
        private readonly WorldBoundsData _worldBoundsData;

        public WorldFrame(WorldBoundsData worldBoundsData)
        {
            _worldBoundsData = worldBoundsData;
        }

        public void Draw()
        {
            float halfX = _worldBoundsData.MapSize.x * 0.5f;
            float halfY = _worldBoundsData.MapSize.y * 0.5f;
            GameObject root = new GameObject("World Frame");
            LineRenderer line = root.AddComponent<LineRenderer>();

            line.widthMultiplier = _worldBoundsData.VisualData.LineWidth;
            line.material = _worldBoundsData.VisualData.LineMaterial;
            line.loop = true;
            line.sortingOrder = -1;

            Vector3[] positions = new Vector3[]
            {
                new Vector3(-halfX, -halfY, 0f),
                new Vector3(halfX, -halfY, 0f),
                new Vector3(halfX, halfY, 0f),
                new Vector3(-halfX, halfY, 0f),
            };
            line.positionCount = positions.Length;
            line.SetPositions(positions);
        }
    }
}