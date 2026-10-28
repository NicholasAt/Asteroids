using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Bullet;
using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Enemy.Fragments;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Services.EnemySpawner;
using Assets.Scripts.Gameplay.Services.Factories;
using Assets.Scripts.Gameplay.Services.InputService;
using Assets.Scripts.Gameplay.Signals;
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

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<PhysicsWorld>().FromNewComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();

        }
        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<EnemyDeathSignal>();
        }
        private void BindPools()
        {
        }

        private void BindFactories()
        {
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<ShipBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle();
        }
        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
            Container.Bind<WeaponData>().FromInstance(_allData.WeaponData).AsSingle();
            Container.Bind<AsteroidData>().FromInstance(_allData.AsteroidData).AsSingle();
            Container.Bind<BulletData>().FromInstance(_allData.BulletData).AsSingle();
            Container.Bind<FragmentsData>().FromInstance(_allData.FragmentsData).AsSingle();
        }
        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}