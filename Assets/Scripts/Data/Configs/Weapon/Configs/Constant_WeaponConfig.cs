using UnityEngine;

namespace Assets.Scripts.Data.Configs.Weapon.Configs
{
    [CreateAssetMenu(menuName = MenuName + "Constant")]
    public class Constant_WeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int MaxCharges { get; private set; }
        [field: SerializeField] public int CDCharge { get; private set; }
        [field: SerializeField] public float RayWidth { get; private set; }
        [field: SerializeField] public float RayLength { get; private set; }
        [field: SerializeField] public float EffectDuration { get; private set; }
        [field: SerializeField] public GameObject VisualPrefab { get; private set; }
    }
}