using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Services.Advertisement
{
    public interface IAdvertisementService
    {
        UniTask Init();
        void ShowBanner();
        void HideBanner();
    }
}