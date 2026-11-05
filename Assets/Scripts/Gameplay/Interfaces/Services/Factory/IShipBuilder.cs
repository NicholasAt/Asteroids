using Assets.Scripts.Data.Configs.Weapon;
using System.Collections.Generic;

namespace Assets.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IShipBuilder
    {
        void CreateShip(IReadOnlyList<WeaponId> weaponIds);
    }
}