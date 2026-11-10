using Assets.Game.Scripts.Core.Bootstrap.States;
using Assets.Game.Scripts.Core.Interfaces.States;
using Assets.Game.Scripts.Data.Configs.Constants;
using Assets.Game.Scripts.Services.Advertisement;
using Assets.Game.Scripts.Services.Analytics;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Core.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        private const string AnalyticsServiceLogName = "Analytics";
        private const string AdvertisementServiceLogName = "Advertisement";

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

        private void Start()
        {
            foreach (GameObject target in _dontDestroyOnLoad)
            {
                target.transform.parent = null;
                DontDestroyOnLoad(target);
            }

            LoadLevelAsync().Forget();
            InitServicesAsync().Forget();
        }

        private async UniTask LoadLevelAsync()
        {
            try
            {
                await _gameStateMachine.Enter<LoadLevelState, string>(SceneConstants.LevelSceneKey);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private async UniTask InitServicesAsync()
        {
            await UniTask.WhenAll(
                InitServiceAsync(AnalyticsServiceLogName, _analyticsService.Init),
                InitServiceAsync(AdvertisementServiceLogName, _advertisement.Init));
        }

        private async UniTask InitServiceAsync(string serviceName, Func<UniTask> init)
        {
            try
            {
                await init();
            }
            catch (Exception e)
            {
                Debug.LogError($"{serviceName} initialization failed, the game continues without it: {e}");
            }
        }
    }
}