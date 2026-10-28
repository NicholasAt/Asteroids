using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ShipBuilder _shipBuilder;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemySpawner _enemySpawner;

        public LoadLevelState(SceneLoader sceneLoader, ShipBuilder shipBuilder, IEnemyFactory enemyFactory, IEnemySpawner enemySpawner)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _enemySpawner.Preparation();
            _shipBuilder.CreateShip(weaponId1: WeaponId.Laser, weaponId2: WeaponId.Gun);
            _enemyFactory.CreateAsteroid(new Vector2(5, 7), Vector2.left + Vector2.down, 0.0f, 0.2f, 0);
        }
    }
}