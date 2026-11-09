using Assets.Game.Scripts.Core.Interfaces.States;
using Assets.Game.Scripts.Core.Services;
using Assets.Game.Scripts.Data.Configs.Game;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Models;
using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.UI.Services;
using Assets.Game.Scripts.Gameplay.UI.ViewModels;
using Assets.Scripts.Services.Advertisement;
using Assets.Scripts.Services.Analytics;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly PlayerSettings _playerSettings;
        private readonly GameData _gameData;
        private readonly HUDViewModel _hUDViewModel;
        private readonly IShipBuilder _shipBuilder;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uIFactory;
        private readonly IWorldFrame _worldFrame;
        private readonly IAnalyticsService _analytics;
        private readonly IAdvertisementService _advertisement;

        public LoadLevelState(SceneLoader sceneLoader, IShipBuilder shipBuilder, PlayerSettings playerSettings, GameData gameData, HUDViewModel hUDViewModel, IEnemyFactory enemyFactory, IEnemySpawner enemySpawner, IProgressService progressService, IUIFactory uIFactory, IWorldFrame worldFrame, IAnalyticsService analyticsService, IAdvertisementService advertisement)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _playerSettings = playerSettings;
            _gameData = gameData;
            _hUDViewModel = hUDViewModel;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
            _progressService = progressService;
            _uIFactory = uIFactory;
            _worldFrame = worldFrame;
            _analytics = analyticsService;
            _advertisement = advertisement;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            InitProgress();
            _shipBuilder.CreateShip(GetWeaponInds());
            _enemySpawner.Start();
            _uIFactory.CreateHUD();

            if (_gameData.IsMobile)
                _uIFactory.CreateMobileInput();

            _worldFrame.Draw();
            _analytics.SendStartGame();
            _advertisement.ShowBanner();
        }

        private void InitProgress()
        {
            _progressService.SetScore(new ScoreProgress());
            _progressService.SetShip(CreateProgress());
            _hUDViewModel.Preparation();
        }

        private List<WeaponId> GetWeaponInds()
        {
            return new List<WeaponId>() { WeaponId.Gun_Gun, WeaponId.Constant_Laser };
        }

        private ShipProgress CreateProgress()
        {
            _progressService.ShipProgress?.Dispose();

            Health health = new(_playerSettings.StartHealth);
            ShipProgress progress = new ShipProgress(health);
            return progress;
        }
    }
}