using System.Collections.Generic;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface IWeaponKeeper
    {
        void SetWeapon(IReadOnlyList<IWeapon> weapons);
    }
}