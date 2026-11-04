using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Ship;
using System.Collections.Generic;

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

        public ShipView CreateShip(IReadOnlyList<WeaponId> weaponIds)
        {
            (ShipView view, Ship.Ship ship) = _shipFactory.Create();
            List<IWeaponModel> weaponModels = new();
            List<IWeapon> weapons = new();
            
            foreach (WeaponId weaponId in weaponIds)
            {
                (IWeapon weapon, IWeaponModel weaponModel) = _weaponFactory.CreateWeapon(weaponId, view.transform, ship.PhysicsBody);
                weapons.Add(weapon);
                if (weaponModel != null)
                    weaponModels.Add(weaponModel);
            }

            ship.SetWeapon(weapons);
            _progressService.ShipProgress.SetWeapons(weaponModels);

            return view;
        }
    }
}