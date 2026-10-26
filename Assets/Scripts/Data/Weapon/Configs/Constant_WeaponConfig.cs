using UnityEngine;

namespace Assets.Scripts.Data.Weapon.Configs
{
    [CreateAssetMenu(menuName = MenuName + "Constant")]
    public class Constant_WeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public GameObject VisualPrefab { get; private set; }
    }
}