using Assets.Scripts.Data.Configs.Bullet;
using Assets.Scripts.Data.Configs.Enemy.Asteroid;
using Assets.Scripts.Data.Configs.Enemy.Fragments;
using Assets.Scripts.Data.Configs.Enemy.Saucer;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Configs.UI;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Configs.WorldBounds;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Data/Core/All data")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public ShipData ShipData { get; private set; }
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public AsteroidData AsteroidData { get; private set; }
        [field: SerializeField] public FragmentsData FragmentsData { get; private set; }
        [field: SerializeField] public SaucerData SaucerData { get; private set; }
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public WorldBoundsData WorldBoundsData { get; private set; }
        [field: SerializeField] public WindowsData WindowsData { get; private set; }
    }
}