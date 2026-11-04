using Assets.Scripts.Core.Common;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UI.ViewModels
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
        public readonly ReactiveProperty<float> LaserCooldown = new ReactiveProperty<float>();

        private readonly IProgressService _progressService;
        private readonly PlayerData _playerData;
        private ShipProgress _shipProgress;
        private ScoreProgress _scoreProgress;
        private Constant_WeaponModel _constantModel;

        public HUDViewModel(IProgressService progressService, PlayerData playerData)
        {
            _progressService = progressService;
            _playerData = playerData;
        }

        public void Preparation()
        {
            _shipProgress = _progressService.ShipProgress;
            _scoreProgress = _progressService.ScoreProgress;

            _scoreProgress.OnScoreChange += RefreshScore;
            _shipProgress.OnWeaponChanged += RefreshWeapons;

            MaxHealth.Value = _playerData.StartHealth;
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
                LaserCooldown.Value = _constantModel.CurrentCD;
                LaserCharges.Value = _constantModel.CurrentCharges;
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
                        case Constant_WeaponModel constantModel:
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