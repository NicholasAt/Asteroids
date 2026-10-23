using Assets.Scripts.Data.AllData;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Services.Factories;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private AllData _allData;
        public override void InstallBindings()
        {
            Container.Bind<ShipData>().FromInstance(_allData.ShipData).AsSingle();

            //Container.BindFactory<GameObject, ShipFactory>().FromFactory<ShipFactory>();
            //Container.Bind<IFactory<GameObject>>().To<ShipFactory>().AsSingle();
        }
    }
}