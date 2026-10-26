using UnityEngine;

namespace Assets.Scripts.Data.Ship
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Ship data")]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public GameObject ShipPrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 5;
    }
}