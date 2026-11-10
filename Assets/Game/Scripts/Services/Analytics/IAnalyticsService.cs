using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Analytics
{
    public interface IAnalyticsService
    {
        UniTask Init();
        void SendStartGame();
    }
}