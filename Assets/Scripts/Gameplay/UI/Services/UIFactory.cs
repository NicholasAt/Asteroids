using Assets.Scripts.Data.Configs.UI;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.UI.Services;
using Zenject;

namespace Assets.Scripts.Gameplay.UI.Services
{
    public class UIFactory: IUIFactory
    {
        private readonly DiContainer _diContainer;
        private readonly WindowsData _windowsData;

        public UIFactory(DiContainer diContainer, WindowsData windowsData)
        {
            _diContainer = diContainer;
            _windowsData = windowsData;
        }

        public void CreateHUD()
        {
            UnityEngine.GameObject instance = _diContainer.InstantiatePrefab(_windowsData.HUDPrefab);
            instance.ToMoveToActiveScene();
        }

        public void CreateMobileInput()
        {
            UnityEngine.GameObject instance = _diContainer.InstantiatePrefab(_windowsData.MobileInputPrefab);
            instance.ToMoveToActiveScene();
        }
    }
}