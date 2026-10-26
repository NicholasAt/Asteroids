using Assets.Scripts.Data.Weapon.Configs;

namespace Assets.Scripts.Gameplay.Weapon.Constant
{
    public class Constant_Weapon : BaseWeapon
    {
        private readonly Constant_WeaponConfig _config;
        private readonly Constant_WeaponEffect _weaponEffect;
        private bool _attackToggle;

        public Constant_Weapon(Constant_WeaponConfig config, Constant_WeaponEffect weaponEffect)
        {
            _config = config;
            _weaponEffect = weaponEffect;
        }

        public override void Update(bool isPress)
        {
            if (isPress)
            {
                _weaponEffect.Update();
            }

            if (_attackToggle == false && isPress)
            {
                _attackToggle = true;
            }
            else if (_attackToggle && isPress == false)
            {
                _attackToggle = false;
                _weaponEffect.Stop();
            }
        }
    }
}