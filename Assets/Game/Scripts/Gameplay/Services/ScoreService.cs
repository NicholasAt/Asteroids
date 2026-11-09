using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services
{
    public class ScoreService : IScoreService
    {
        private readonly EnemySettings _enemySettings;
        private readonly SignalBus _signalBus;
        private readonly IProgressService _progressService;
        private readonly Dictionary<EnemyType, int> _rewards;

        public ScoreService(EnemySettings enemySettings, SignalBus signalBus, IProgressService progressService)
        {
            _enemySettings = enemySettings;
            _signalBus = signalBus;
            _progressService = progressService;
            _rewards = new Dictionary<EnemyType, int>();
            foreach (EnemyType type in System.Enum.GetValues(typeof(EnemyType)))
            {
                if (type != EnemyType.None)
                    _rewards[type] = _enemySettings.Get(type).Reward;
            }
            _signalBus.Subscribe<EnemyDeathSignal>(EnemyDie);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<EnemyDeathSignal>(EnemyDie);
        }

        private void EnemyDie(EnemyDeathSignal signal)
        {
            _progressService.ScoreProgress.IncrementScore(GetReward(signal.EnemyType));
        }
        private int GetReward(EnemyType type)
        {
            return _rewards.TryGetValue(type, out int reward) ? reward : LogError(type);
        }
        private int LogError(EnemyType type)
        {
            Debug.LogError($"zero reward [{type}]");
            return 0;
        }
    }
}