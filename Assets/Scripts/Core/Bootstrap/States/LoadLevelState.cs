using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ShipBuilder _shipBuilder;
        private readonly EnemyFactory _enemyFactory;

        public LoadLevelState(SceneLoader sceneLoader, ShipBuilder shipBuilder, EnemyFactory enemyFactory)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _enemyFactory = enemyFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _shipBuilder.CreateShip(weaponId1: WeaponId.Laser, weaponId2: WeaponId.Gun);
            _enemyFactory.CreateAsteroid(new Vector2(12, 15), Vector2.left + Vector2.down, 3);
        }
    }
}