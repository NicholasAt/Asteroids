using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IWeaponKeeper
    {
        void SetWeapon(IReadOnlyList<IWeapon> weapons);
    }
}