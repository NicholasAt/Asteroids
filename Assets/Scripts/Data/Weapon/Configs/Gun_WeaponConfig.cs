using UnityEngine;

namespace Assets.Scripts.Data.Weapon.Configs
{
    [CreateAssetMenu(menuName = MenuName + "Gun")]
    public class Gun_WeaponConfig : BaseWeaponConfig
    {
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public float CD { get; private set; } 
        [field: SerializeField] public float BulletSpeedLimit{ get; private set; }
        [field: SerializeField] public float ForwardSpawnPointOffset { get; private set; } 
    }
}