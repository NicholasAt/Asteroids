using Assets.Game.Scripts.Data.Configs.UI;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.UI.Services;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.UI.Services
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
            CreateWindow(_windowsData.HUDPrefab);
        }

        public void CreateMobileInput()
        {
            CreateWindow(_windowsData.MobileInputPrefab);
        }

        private void CreateWindow(GameObject prefab)
        {
            GameObject instance = _diContainer.InstantiatePrefab(prefab);
            instance.ToMoveToActiveScene();
        }
    }
}