using Assets.Game.Scripts.Core.Bootstrap.States;
using Assets.Game.Scripts.Core.Services;
using Assets.Game.Scripts.Data.Configs;
using Assets.Game.Scripts.Data.Configs.Bullet;
using Assets.Game.Scripts.Data.Configs.Constants;
using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Game;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Configs.Ship;
using Assets.Game.Scripts.Data.Configs.UI;
using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Configs.WorldBounds;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Services;
using Assets.Game.Scripts.Gameplay.Services.Factories;
using Assets.Game.Scripts.Gameplay.Services.Factories.Weapon;
using Assets.Game.Scripts.Gameplay.Services.InputService;
using Assets.Game.Scripts.Gameplay.Services.Pools;
using Assets.Game.Scripts.Gameplay.Services.Register;
using Assets.Game.Scripts.Gameplay.Signals;
using Assets.Game.Scripts.Gameplay.UI.Services;
using Assets.Game.Scripts.Gameplay.UI.ViewModels;
using Assets.Scripts.Services.Advertisement;
using Assets.Scripts.Services.Analytics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Core.Bootstrap
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
            Container.BindInterfacesAndSelfTo<WorldFrame>().AsSingle();

            Container.BindInterfacesAndSelfTo<TickService>().FromNewComponentOn(gameObject).AsSingle();

            BindServices();
        }
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<EnemyDeathSignal>();
            Container.DeclareSignal<PlayerDeathSignal>();
        }
        private void BindInput()
        {
            if (_allData.GameData.IsMobile)
                Container.BindInterfacesAndSelfTo<MobileInputViewModel>().AsSingle();
            else
                Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();
        }
        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdvertisementService>().AsSingle();
        }
        private void BindPools()
        {
            Container.BindInterfacesAndSelfTo<BulletPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyPool>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindInterfacesAndSelfTo<ShipBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShipFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle();

            Container.Bind<IWeaponCreator>().To<GunCreator>().AsSingle();
            Container.Bind<IWeaponCreator>().To<ConstantCreator>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
        }

        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
            Container.Bind<GameData>().FromInstance(_allData.GameData).AsSingle();
            Container.Bind<EnemyData>().FromInstance(_allData.EnemyData).AsSingle();
            Container.Bind<WeaponData>().FromInstance(_allData.WeaponData).AsSingle();
            Container.Bind<BulletData>().FromInstance(_allData.BulletData).AsSingle();
            Container.Bind<WorldBoundsData>().FromInstance(_allData.WorldBoundsData).AsSingle();
            Container.Bind<WindowsData>().FromInstance(_allData.WindowsData).AsSingle();

            Container.Bind<PlayerSettings>().FromInstance(JsonConfigLoader.Load<PlayerSettings>(PathConstants.PlayerConfig)).AsSingle();
            Container.Bind<EnemySettings>().FromInstance(JsonConfigLoader.Load<EnemySettings>(PathConstants.EnemiesConfig)).AsSingle();
            Container.Bind<WorldSettings>().FromInstance(JsonConfigLoader.Load<WorldSettings>(PathConstants.WorldConfig)).AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
        }
        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<HUDViewModel>().AsSingle();
        }
    }
}