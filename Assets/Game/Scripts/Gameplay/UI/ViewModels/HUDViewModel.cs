using Assets.Game.Scripts.Core.Common;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Models;
using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Data.Models.Weapon;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.UI.ViewModels
{
    public class HUDViewModel : IDisposable, ICustomTick
    {
        public readonly ReactiveProperty<int> Score = new ReactiveProperty<int>();

        public readonly ReactiveProperty<int> MaxHealth = new ReactiveProperty<int>();
        public readonly ReactiveProperty<int> CurrentHealth = new ReactiveProperty<int>();

        public readonly ReactiveProperty<Vector2> Position = new ReactiveProperty<Vector2>();
        public readonly ReactiveProperty<float> Rotate = new ReactiveProperty<float>();
        public readonly ReactiveProperty<float> Speed = new ReactiveProperty<float>();

        public readonly ReactiveProperty<int> LaserCharges = new ReactiveProperty<int>();
        public readonly ReactiveProperty<int> MaxLaserCharges = new ReactiveProperty<int>();
        public readonly ReactiveProperty<float> LaserCooldown = new ReactiveProperty<float>();

        private readonly IProgressService _progressService;
        private readonly PlayerSettings _playerSettings;

        private ShipProgress _shipProgress;
        private ScoreProgress _scoreProgress;
        private ConstantWeaponModel _constantModel;

        public HUDViewModel(IProgressService progressService, PlayerSettings playerSettings)
        {
            _progressService = progressService;
            _playerSettings = playerSettings;
        }

        public void Preparation()
        {
            _shipProgress = _progressService.ShipProgress;
            _scoreProgress = _progressService.ScoreProgress;

            _scoreProgress.OnScoreChange += RefreshScore;
            _shipProgress.OnWeaponChanged += RefreshWeapons;

            MaxHealth.Value = _playerSettings.StartHealth;
            RefreshWeapons();
        }

        public void Dispose()
        {
            if (_shipProgress != null)
                _shipProgress.OnWeaponChanged -= RefreshWeapons;
            if (_scoreProgress != null)
                _scoreProgress.OnScoreChange -= RefreshScore;
        }

        public void Tick(float _)
        {
            if (_shipProgress != null)
            {
                Position.Value = _shipProgress.Position;
                Rotate.Value = Mathf.Repeat(_shipProgress.Rotate * Mathf.Rad2Deg, 360f);
                Speed.Value = _shipProgress.CurrentSpeed;

                CurrentHealth.Value = _shipProgress.Health.CurrentHealth;
            }

            if (_constantModel != null)
            {
                MaxLaserCharges.Value = _constantModel.MaxCharges;
                LaserCharges.Value = _constantModel.CurrentCharges;
                LaserCooldown.Value = _constantModel.CooldownRemaining;
            }
        }

        private void RefreshScore(int score)
        {
            Score.Value = score;
        }

        private void RefreshWeapons()
        {
            if (_shipProgress?.Weapons != null)
            {
                foreach (IWeaponModel weaponModel in _shipProgress.Weapons)
                {
                    switch (weaponModel)
                    {
                        case ConstantWeaponModel constantModel:
                            _constantModel = constantModel;
                            break;

                        default:
                            Debug.LogError("zero logic");
                            break;
                    }
                }
            }
        }
    }
}