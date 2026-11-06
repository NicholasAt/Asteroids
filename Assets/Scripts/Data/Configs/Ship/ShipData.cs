using UnityEngine;

namespace Assets.Scripts.Data.Configs.Ship
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Ship data")]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public GameObject ShipPrefab { get; private set; }
    }
}
