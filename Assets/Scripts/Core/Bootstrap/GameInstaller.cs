using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Core.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Data.Bullet;
using Assets.Scripts.Data.Enemy.Asteroid;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Physics;
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
            BindPools();

            Container.Bind<SceneLoader>().AsSingle();
            Container.Bind<PhysicsWorld>().FromNewComponentOn(gameObject).AsSingle();
            Container.BindInterfacesAndSelfTo<PCInputService>().AsSingle();

        }
        private void BindPools()
        {
        }

        private void BindFactories()
        {
            Container.Bind<ShipFactory>().AsSingle();
            Container.Bind<WeaponFactory>().AsSingle();
            Container.Bind<ShipBuilder>().AsSingle();
            Container.Bind<EnemyFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletFactory>().AsSingle();
        }
        private void BindData()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();
            Container.Bind<WeaponData>().FromInstance(_allData.WeaponData).AsSingle();
            Container.Bind<AsteroidData>().FromInstance(_allData.AsteroidData).AsSingle();
            Container.Bind<BulletData>().FromInstance(_allData.BulletData).AsSingle();
        }
        private void BindStates()
        {
            Container.Bind<LoadLevelState>().AsSingle();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}