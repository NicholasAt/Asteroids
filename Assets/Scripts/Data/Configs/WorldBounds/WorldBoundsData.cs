using UnityEngine;

namespace Assets.Scripts.Data.Configs.WorldBounds
{
    [CreateAssetMenu(menuName = "Data/Gameplay/World bounds")]
    public class WorldBoundsData : ScriptableObject
    {
        [field: SerializeField] public WorldBoundsVisualData VisualData { get; private set; }
    }
}
