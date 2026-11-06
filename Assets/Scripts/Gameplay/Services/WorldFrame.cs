using Assets.Scripts.Data.Configs.Settings;
using Assets.Scripts.Data.Configs.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services
{
    public class WorldFrame : IWorldFrame
    {
        private readonly WorldBoundsData _worldBoundsData;
        private readonly WorldSettings _worldSettings;

        public WorldFrame(WorldBoundsData worldBoundsData, WorldSettings worldSettings)
        {
            _worldBoundsData = worldBoundsData;
            _worldSettings = worldSettings;
        }

        public void Draw()
        {
            float halfX = _worldSettings.MapSize.x * 0.5f;
            float halfY = _worldSettings.MapSize.y * 0.5f;
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