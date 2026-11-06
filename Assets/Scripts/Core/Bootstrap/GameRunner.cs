using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Interfaces.States;
using Assets.Scripts.Data.Configs.Constants;
using Assets.Scripts.Services.Analytics;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _dontDestroyOnLoad;
        private IGameStateMachine _gameStateMachine;
        private IAnalyticsService _analyticsService;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IAnalyticsService analyticsService)
        {
            _gameStateMachine = gameStateMachine;
            _analyticsService = analyticsService;
        }
        private async void Start()
        {
            foreach (GameObject target in _dontDestroyOnLoad)
            {
                target.transform.parent = null;
                DontDestroyOnLoad(target);
            }
            await _analyticsService.Init();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConstants.LevelSceneKey);
        }
    }
}