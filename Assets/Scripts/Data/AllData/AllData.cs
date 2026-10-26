using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
using UnityEngine;

namespace Assets.Scripts.Data.AllData
{
    [CreateAssetMenu(menuName = "Data/Core/All data")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public ShipData ShipData { get; private set; }
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
    }
}