using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Factory;
using System.Collections.Generic;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipBuilder: IShipBuilder
    {
        private readonly IShipFactory _shipFactory;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IProgressService _progressService;
        private readonly ShipData _shipData;

        public ShipBuilder(IShipFactory shipFactory, IWeaponFactory weaponFactory, IProgressService progressService, ShipData shipData)
        {
            _shipFactory = shipFactory;
            _weaponFactory = weaponFactory;
            _progressService = progressService;
            _shipData = shipData;
        }

        public void CreateShip(IReadOnlyList<WeaponId> weaponIds)
        {
            (Physics.PhysicsBody body, UnityEngine.Transform transform, IWeaponKeeper weaponKeeper) = _shipFactory.Create();
            List<IWeaponModel> weaponModels = new();
            List<IWeapon> weapons = new();

            foreach (WeaponId weaponId in weaponIds)
            {
                (IWeapon weapon, IWeaponModel weaponModel) = _weaponFactory.CreateWeapon(weaponId, transform, body);
                weapons.Add(weapon);
                if (weaponModel != null)
                    weaponModels.Add(weaponModel);
            }

            weaponKeeper.SetWeapon(weapons);
            _progressService.ShipProgress.SetWeapons(weaponModels);
        }
    }
}