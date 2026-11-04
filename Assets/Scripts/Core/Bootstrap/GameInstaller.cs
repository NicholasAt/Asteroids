using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Configs.Bullet;
using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Data.Configs.Game;
using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Data.Configs.UI;
using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Configs.WorldBounds;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Services;
using Assets.Scripts.Gameplay.Services.EnemySpawner;
using Assets.Scripts.Gameplay.Services.Factories;
using Assets.Scripts.Gameplay.Services.Factories.Weapon;
using Assets.Scripts.Gameplay.Services.InputService;
using Assets.Scripts.Gameplay.Services.Progress;
using Assets.Scripts.Gameplay.Services.Register;
using Assets.Scripts.Gameplay.Services.Tick;
using Assets.Scripts.Gameplay.Services.WorldBounds;
using Assets.Scripts.Gameplay.Signals;
using Assets.Scripts.Gameplay.UI.Services;
using Assets.Scripts.Gameplay.UI.ViewModels;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AllData _allData;
        public override void InstallBindings()
        {
            BindSignals();

            BindData();
            BindStates();

            BindFactories();
            BindPools();

            BindUI();
            BindInput();

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<PhysicsWorld>().AsSingle();

            Container.BindInterfacesAndSelfTo<ScoreService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProgressService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRegistry>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<WorldBounds>().AsSingle();

            Container.BindInterfacesAndSelfTo<TickService>().FromNewComponentOn(gameObject).AsSingle();
        }
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<EnemyDeathSignal>();
        }
        private void BindInput()
        {
            if (_allData.GameData.IsMobile)
                Container.BindInterfacesAndSelfTo<MobileInputViewModel>().AsSingle();
            else
                Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();
        }

        private void BindPools()
        {
        }

        private void BindFactories()
        {
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<ShipBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle();

            Container.Bind<IWeaponCreator>().To<GunCreator>().AsSingle();
            Container.Bind<IWeaponCreator>().To<ConstantCreator>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
        }

        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
            Container.Bind<PlayerData>().FromInstance(_allData.PlayerData).AsSingle();
            Container.Bind<GameData>().FromInstance(_allData.GameData).AsSingle();
            Container.Bind<EnemyData>().FromInstance(_allData.EnemyData).AsSingle();
            Container.Bind<WeaponData>().FromInstance(_allData.WeaponData).AsSingle();
            Container.Bind<BulletData>().FromInstance(_allData.BulletData).AsSingle();
            Container.Bind<WorldBoundsData>().FromInstance(_allData.WorldBoundsData).AsSingle();
            Container.Bind<WindowsData>().FromInstance(_allData.WindowsData).AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<HUDViewModel>().AsSingle();
        }
    }
}