using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.UI.Services;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

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
        private readonly IUIFactory _uIFactory;

        public LoadLevelState(SceneLoader sceneLoader, ShipBuilder shipBuilder, PlayerData playerData, IEnemyFactory enemyFactory, IEnemySpawner enemySpawner, IProgressService progressService, IUIFactory uIFactory)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _playerData = playerData;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
            _progressService = progressService;
            _uIFactory = uIFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _progressService.SetShip(CreateProgress());
            _shipBuilder.CreateShip(GetWeaponInds());
            _enemySpawner.Start();
            _uIFactory.CreateHUD();
        }
        private List<WeaponId> GetWeaponInds()
        {
            return new List<WeaponId>() { WeaponId.Constant_Laser, WeaponId.Gun_Gun };
        }
        private ShipProgress CreateProgress()
        {
            _progressService.ShipProgress?.Dispose();

            Health health = new(_playerData.StartHealth);
            ShipProgress progress = new ShipProgress(health);
            return progress;
        }
    }
}