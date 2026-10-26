using Assets.Scripts.Data.Weapon.Configs;

namespace Assets.Scripts.Gameplay.Weapon.Gun
{
    public class Gun_Weapon : BaseWeapon
    {
        private readonly Gun_WeaponConfig _config;

        public Gun_Weapon(Gun_WeaponConfig config)
        {
            _config = config;
        }
        public override void Update(bool isPress)
        {
        }
    }
}