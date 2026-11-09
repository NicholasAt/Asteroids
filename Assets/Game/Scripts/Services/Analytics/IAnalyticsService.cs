using Cysharp.Threading.Tasks;
namespace Assets.Scripts.Services.Analytics
{
    public interface IAnalyticsService
    {
        UniTask Init();
        void SendStartGame();
    }
}