using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Data.Player;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ShipBuilder _shipBuilder;
        private readonly PlayerData _playerData;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IProgressService _progressService;

        public LoadLevelState(SceneLoader sceneLoader, ShipBuilder shipBuilder, PlayerData playerData, IEnemyFactory enemyFactory, IEnemySpawner enemySpawner, IProgressService progressService)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _playerData = playerData;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _progressService.SetShip(CreateProgress());
            _shipBuilder.CreateShip(weaponId1: WeaponId.Constant_Laser, weaponId2: WeaponId.Gun_Gun);
            _enemySpawner.Start();
        }

        private ShipProgress CreateProgress()
        {
            Health health = new(_playerData.StartHealth);
            ShipProgress progress = new ShipProgress(health);
            return progress;
        }
    }
}