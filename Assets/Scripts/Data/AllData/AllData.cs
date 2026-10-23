using Assets.Scripts.Data.Ship;
using UnityEngine;

namespace Assets.Scripts.Data.AllData
{
    [CreateAssetMenu(menuName = "Data/Core/All data")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public ShipData ShipData { get; private set; }
    }
}