using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.AllData;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Services.Factories;
using Assets.Scripts.Gameplay.Services.InputService;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AllData _allData;
        public override void InstallBindings()
        {
            BindData();
            BindStates();

            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();
            Container.Bind<ShipFactory>().AsSingle();
        }
        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
        }
        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}