using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Services.Advertisement
{
    public interface IAdvertisementService
    {
        UniTask Init();
        void ShowBanner();
    }
}