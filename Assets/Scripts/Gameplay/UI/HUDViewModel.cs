using Assets.Scripts.Core.Common;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Data.Models.Weapon;
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

        public readonly ReactiveProperty<int> MaxHealth = new ReactiveProperty<int>();
        public readonly ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

        public readonly ReactiveProperty<Vector2> Position = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<float> Rotate = new ReactiveProperty<float>();

        public readonly ReactiveProperty<int> LaserCharges = new ReactiveProperty<int>();
        public readonly ReactiveProperty<float> LaserCooldown = new ReactiveProperty<float>();

        public HUDViewModel(IProgressService progressService, PlayerData playerData)
        {
            _progressService = progressService;
            _playerData = playerData;
            _progressService.OnShipProgressCreated += OnShipProgressCreated;
        }

        public void Dispose()
        {
            _progressService.OnShipProgressCreated -= OnShipProgressCreated;

            if (_shipProgress != null)
                _shipProgress.OnWeaponChanged -= RefreshWeapons;
        }

        public void Tick(float _)
        {
            if (_shipProgress != null)
            {
                Position.Value = _shipProgress.Position;
                Rotate.Value = Mathf.Repeat(_shipProgress.Rotate * Mathf.Rad2Deg, 360f);

                CurrentHealth.Value = _shipProgress.Health.CurrentHealth;
            }
        }

        private void OnShipProgressCreated(ShipProgress progress)
        {
            _shipProgress = progress;
            MaxHealth.Value = _playerData.StartHealth;
            _shipProgress.OnWeaponChanged += RefreshWeapons;
        }

        private void RefreshWeapons()
        {
            foreach (IWeaponModel weaponModel in _shipProgress.Weapons)
            {
                switch (weaponModel)
                {
                    case Constant_WeaponModel constantModel:
                        break;

                    default:
                        Debug.LogError("zero logic");
                        break;
                }
            }
        }
    }
}