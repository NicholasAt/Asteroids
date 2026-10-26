using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Ship;
using Assets.Scripts.Gameplay.Weapon;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipBuilder
    {
        private readonly ShipFactory _shipFactory;
        private readonly WeaponFactory _weaponFactory;
        private readonly ShipData _shipData;

        public ShipBuilder(ShipFactory shipFactory, WeaponFactory weaponFactory, ShipData shipData)
        {
            _shipFactory = shipFactory;
            _weaponFactory = weaponFactory;
            _shipData = shipData;
        }

        public ShipView CreateShip(WeaponId weaponId1, WeaponId weaponId2)
        {
            (ShipView view, Ship.Ship ship) = _shipFactory.Create();
            IWeapon weapon1 = _weaponFactory.CreateWeapon(weaponId1, view.transform);
            IWeapon weapon2 = _weaponFactory.CreateWeapon(weaponId2, view.transform);
            ship.SetWeapon(weapon1, weapon2);

            return view;
        }
    }
}