using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Interfaces.States;
using Assets.Scripts.Data.Configs.Constants;
using Assets.Scripts.Services.Advertisement;
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
        private IAdvertisementService _advertisement;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IAnalyticsService analyticsService, IAdvertisementService advertisement)
        {
            _gameStateMachine = gameStateMachine;
            _analyticsService = analyticsService;
            _advertisement = advertisement;
        }
        private async void Start()
        {
            foreach (GameObject target in _dontDestroyOnLoad)
            {
                target.transform.parent = null;
                DontDestroyOnLoad(target);
            }
            await _analyticsService.Init();
            await _advertisement.Init();
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConstants.LevelSceneKey);
        }
    }
}