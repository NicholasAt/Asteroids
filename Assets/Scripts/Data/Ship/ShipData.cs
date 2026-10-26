using UnityEngine;

namespace Assets.Scripts.Data.Ship
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Ship data")]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public GameObject ShipPrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 5;
        [field: SerializeField] public float Mass { get; private set; }
        [field: SerializeField] public float Drag { get; private set; }
    }
}