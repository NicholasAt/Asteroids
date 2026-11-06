using UnityEngine;

namespace Assets.Scripts.Data.Configs.Weapon.Configs
{
    [CreateAssetMenu(menuName = MenuName + "Constant")]
    public class Constant_WeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public GameObject VisualPrefab { get; private set; }
    }
}
