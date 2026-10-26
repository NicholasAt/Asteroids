using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Weapon;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipBuilder
    {
        private readonly ShipFactory _shipFactory;
        private readonly WeaponFactory _weaponFactory;

        public ShipBuilder(ShipFactory shipFactory, WeaponFactory weaponFactory)
        {
            _shipFactory = shipFactory;
            _weaponFactory = weaponFactory;
        }

        public Ship.Ship CreateShip(WeaponId weaponId1, WeaponId weaponId2)
        {
            Ship.Ship ship = _shipFactory.Create();
            IWeapon weapon1 = _weaponFactory.CreateWeapon(weaponId1, ship.transform);
            IWeapon weapon2 = _weaponFactory.CreateWeapon(weaponId2, ship.transform);

            ship.SetWeapon(weapon1, weapon2);
            return ship;
        }
    }
}