using Assets.Game.Scripts.Core.Interfaces.States;
using Assets.Game.Scripts.Core.Services;
using Assets.Game.Scripts.Data.Configs.Game;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Models;
using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.UI.Services;
using Assets.Game.Scripts.Gameplay.UI.ViewModels;
using Assets.Game.Scripts.Services.Advertisement;
using Assets.Game.Scripts.Services.Analytics;
using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly PlayerSettings _playerSettings;
        private readonly GameData _gameData;
        private readonly HUDViewModel _hudViewModel;
        private readonly IShipBuilder _shipBuilder;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uIFactory;
        private readonly IWorldFrame _worldFrame;
        private readonly IAnalyticsService _analytics;
        private readonly IAdvertisementService _advertisement;

        public LoadLevelState(SceneLoader sceneLoader, IShipBuilder shipBuilder, PlayerSettings playerSettings, GameData gameData, HUDViewModel hudViewModel, IEnemySpawner enemySpawner, IProgressService progressService, IUIFactory uIFactory, IWorldFrame worldFrame, IAnalyticsService analyticsService, IAdvertisementService advertisement)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _playerSettings = playerSettings;
            _gameData = gameData;
            _hudViewModel = hudViewModel;
            _enemySpawner = enemySpawner;
            _progressService = progressService;
            _uIFactory = uIFactory;
            _worldFrame = worldFrame;
            _analytics = analyticsService;
            _advertisement = advertisement;
        }

        public async UniTask Enter(string payload)
        {
            await _sceneLoader.Load(payload);
            PrepareLevel();
        }

        private void PrepareLevel()
        {
            InitProgress();
            _shipBuilder.CreateShip(_playerSettings.StartingWeaponIds);
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
            _hudViewModel.Preparation();
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