using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IShipFactory
    {
        (PhysicsBody, Transform, IWeaponKeeper) Create();
    }
}