using UnityEngine;

namespace Assets.Scripts.Data.Configs.WorldBounds
{
    [CreateAssetMenu(menuName = "Data/Gameplay/World bounds")]
    public class WorldBoundsData : ScriptableObject
    {
        [field: SerializeField] public Vector2 MapSize { get; private set; }
        [field: SerializeField] public Vector2 OutMapSize { get; private set; }
        [field: SerializeField] public WorldBoundsVisualData VisualData { get; private set; }
    }
}