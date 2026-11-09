using Assets.Game.Scripts.Data.Configs.Bullet;
using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Game;
using Assets.Game.Scripts.Data.Configs.Ship;
using Assets.Game.Scripts.Data.Configs.UI;
using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Configs.WorldBounds;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs
{
    [CreateAssetMenu(menuName = "Data/Core/All data")]
    public class AllData : ScriptableObject
    {
        [field: SerializeField] public ShipData ShipData { get; private set; }
        [field: SerializeField] public GameData GameData { get; private set; }
        [field: SerializeField] public EnemyData EnemyData { get; private set; }
        [field: SerializeField] public WeaponData WeaponData { get; private set; }
        [field: SerializeField] public BulletData BulletData { get; private set; }
        [field: SerializeField] public WorldBoundsData WorldBoundsData { get; private set; }
        [field: SerializeField] public WindowsData WindowsData { get; private set; }
    }
}