using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UI
{
    public class HUDViewModel : IDisposable, ICustomTick
    {
        private readonly IProgressService _progressService;
        private readonly PlayerData _playerData;
        private ShipProgress _shipProgress;

        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }

        public string PositionText { get; private set; }
        public string RotateText { get; private set; }
        public event Action OnHealthChange;

        public HUDViewModel(IProgressService progressService, PlayerData playerData)
        {
            _progressService = progressService;
            _playerData = playerData;
            _progressService.OnShipProgressCreated += OnShipProgressCreated;
        }

        public void Dispose()
        {
            _progressService.OnShipProgressCreated -= OnShipProgressCreated;
        }

        public void Tick(float _)
        {
            if (_shipProgress != null)
            {
                Vector2 position = _shipProgress.Position;
                float rotate = Mathf.Repeat(_shipProgress.Rotate * Mathf.Rad2Deg, 360f);

                PositionText = $"X: {position.x:F1} Y: {position.y:F1}";
                RotateText = $"Degrees {rotate:F0}";
            }
        }

        private void OnShipProgressCreated(ShipProgress progress)
        {
            if (_shipProgress != null)
                _shipProgress.Health.OnHealthChange -= RefreshHealth;

            _shipProgress = progress;
            MaxHealth = _playerData.StartHealth;
            _shipProgress.Health.OnHealthChange += RefreshHealth;

            RefreshHealth();
        }

        private void RefreshHealth()
        {
            CurrentHealth = _shipProgress.Health.CurrentHealth;
            OnHealthChange?.Invoke();
        }
    }
}