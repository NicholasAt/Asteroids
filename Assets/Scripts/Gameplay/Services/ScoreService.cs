using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Signals;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services
{
    public class ScoreService : IScoreService
    {
        private readonly EnemyData _enemyData;
        private readonly SignalBus _signalBus;
        private readonly IProgressService _progressService;
        private readonly Dictionary<EnemyType, int> _rewards;

        public ScoreService(EnemyData enemyData, SignalBus signalBus, IProgressService progressService)
        {
            _enemyData = enemyData;
            _signalBus = signalBus;
            _progressService = progressService;
            _rewards = _enemyData.Configs.ToDictionary(x => x.EnemyType, x => x.Reward);
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