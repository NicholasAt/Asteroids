using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories.Weapon
{
    public interface IWeaponCreator
    {
        WeaponId[] WeaponIds { get; }
        IWeapon Create(WeaponId weaponId, Transform parent, PhysicsBody physicsBody);
    }

    public class WeaponFactory : IWeaponFactory
    {
        private readonly DiContainer _diContainer;
        private readonly WeaponData _weaponData;
        private readonly Dictionary<WeaponId, IWeaponCreator> _creators = new();
        public WeaponFactory(List<IWeaponCreator> weaponCreators)
        {
            foreach (IWeaponCreator creator in weaponCreators)
            {
                foreach (WeaponId id in creator.WeaponIds)
                {
                    _creators.Add(id, creator);
                }
            }
        }

        public IWeapon CreateWeapon(WeaponId weaponId, Transform parent, PhysicsBody physicsBody)
        {
            return _creators.TryGetValue(weaponId, out IWeaponCreator factory) ? factory.Create(weaponId, parent, physicsBody) : LogError(weaponId);
        }

        private IWeapon LogError(WeaponId weaponId)
        {
            Debug.LogError($"cant create [{weaponId}]");
            return null;
        }
    }
}