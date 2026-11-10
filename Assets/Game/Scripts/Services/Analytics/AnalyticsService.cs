using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private const string StartGameKey = "start_game";

        private FirebaseApp _app;
        private bool _isInitialized;
        private bool _startGamePending;

        public async UniTask Init()
        {
            try
            {
                DependencyStatus dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _isInitialized = true;
                    Debug.Log("Firebase Analytics initialized.");

                    if (_startGamePending)
                    {
                        _startGamePending = false;
                        LogStartGame();
                    }
                }
                else
                {
                    Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            }

            catch (System.Exception e)
            {
                Debug.LogException(e);
            }
        }

        public void SendStartGame()
        {
            if (_isInitialized == false)
            {
                _startGamePending = true;
                return;
            }

            LogStartGame();
        }

        private void LogStartGame()
        {
            FirebaseAnalytics.LogEvent(StartGameKey);
        }
    }
}