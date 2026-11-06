using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using UnityEngine;
namespace Assets.Scripts.Services.Analytics
{
    public class AnalyticsService : IAnalyticsService
    {
        private const string StartGameKey = "start_game";

        private FirebaseApp _app;
        public async UniTask Init()
        {
            try
            {
                DependencyStatus dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask();
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    Debug.Log("Firebase Analytics initialized.");
                }
                else
                {
                    Debug.LogError(System.String.Format("Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            }

            catch (System.Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
        public void SendStartGame()
        {
            FirebaseAnalytics.LogEvent(StartGameKey);
        }
    }
}