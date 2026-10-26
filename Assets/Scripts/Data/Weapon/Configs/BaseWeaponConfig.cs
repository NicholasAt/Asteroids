using UnityEngine;

namespace Assets.Scripts.Data.Weapon.Configs
{
    public abstract class BaseWeaponConfig : ScriptableObject
    {
        public const string MenuName = "Data/Gameplay/Weapon data/Configs/";
        [field: SerializeField] public WeaponId WeaponId { get; private set; }
    }
}