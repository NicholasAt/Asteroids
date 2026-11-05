using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IShipFactory
    {
        (PhysicsBody, Transform, IWeaponKeeper) Create();
    }
}