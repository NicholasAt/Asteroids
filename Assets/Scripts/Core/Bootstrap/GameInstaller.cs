using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.AllData;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
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
            BindFactories();

            Container.Bind<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();

        }
        private void BindFactories()
        {
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<ShipBuilder>().AsSingle();
        }
        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
            Container.Bind<WeaponData>().FromInstance(_allData.WeaponData).AsSingle();
        }
        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}