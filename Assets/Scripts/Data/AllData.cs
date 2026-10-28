using Assets.Scripts.Data.Bullet;
using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Enemy.Fragments;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/Core/All data")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public ShipData ShipData { get; private set; }
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public FragmentsData FragmentsData { get; private set; }
    }
}