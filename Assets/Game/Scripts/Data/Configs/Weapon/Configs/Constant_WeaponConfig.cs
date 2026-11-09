using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Weapon.Configs
{
    [CreateAssetMenu(menuName = MenuName + "Constant")]
    public class Constant_WeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public GameObject VisualPrefab { get; private set; }
    }
}
