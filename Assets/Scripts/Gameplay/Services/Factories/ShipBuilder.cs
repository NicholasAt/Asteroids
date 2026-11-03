using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Ship;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipBuilder
    {
        private readonly ShipFactory _shipFactory;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IProgressService _progressService;
        private readonly ShipData _shipData;

        public ShipBuilder(ShipFactory shipFactory, IWeaponFactory weaponFactory, IProgressService progressService, ShipData shipData)
        {
            _shipFactory = shipFactory;
            _weaponFactory = weaponFactory;
            _progressService = progressService;
            _shipData = shipData;
        }

        public ShipView CreateShip(WeaponId weaponId1, WeaponId weaponId2)
        {
            (ShipView view, Ship.Ship ship) = _shipFactory.Create();
            IWeapon weapon1 = _weaponFactory.CreateWeapon(weaponId1, view.transform, ship.PhysicsBody);
            IWeapon weapon2 = _weaponFactory.CreateWeapon(weaponId2, view.transform, ship.PhysicsBody);
            ship.SetWeapon(weapon1, weapon2);

            return view;
        }
    }
}