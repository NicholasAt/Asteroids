using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Extensions;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory
    {
        private readonly DiContainer _diContainer;
        private readonly ShipData _shipData;

        public ShipFactory(DiContainer diContainer, ShipData shipData)
        {
            _diContainer = diContainer;
            _shipData = shipData;
        }

        public GameObject Create()
        {
            GameObject instance = _diContainer.InstantiatePrefab(_shipData.ShipPrefab);
            instance.ToMoveToActiveScene();
            return instance;
        }
    }
}