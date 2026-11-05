using Assets.Scripts.Core.Interfaces.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Configs.Game;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Scripts.Gameplay.Interfaces.UI.Services;
using Assets.Scripts.Gameplay.UI.ViewModels;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly PlayerData _playerData;
        private readonly GameData _gameData;
        private readonly HUDViewModel _hUDViewModel;
        private readonly IShipBuilder _shipBuilder;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IEnemySpawner _enemySpawner;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uIFactory;
        private readonly IWorldFrame _worldFrame;

        public LoadLevelState(SceneLoader sceneLoader, IShipBuilder shipBuilder, PlayerData playerData, GameData gameData, HUDViewModel hUDViewModel, IEnemyFactory enemyFactory, IEnemySpawner enemySpawner, IProgressService progressService, IUIFactory uIFactory, IWorldFrame worldFrame)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
            _playerData = playerData;
            _gameData = gameData;
            _hUDViewModel = hUDViewModel;
            _enemyFactory = enemyFactory;
            _enemySpawner = enemySpawner;
            _progressService = progressService;
            _uIFactory = uIFactory;
            _worldFrame = worldFrame;
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
        }

        private void InitProgress()
        {
            _progressService.SetScore(new ScoreProgress());
            _progressService.SetShip(CreateProgress());
            _hUDViewModel.Preparation();
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