using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Physics;
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

        public (Ship.ShipView, Ship.Ship) Create()
        {
            GameObject instance = _diContainer.InstantiatePrefab(_shipData.ShipPrefab);
            instance.ToMoveToActiveScene();

            PhysicsBody physicsBody = new PhysicsBody()
            {
                Drag = _shipData.Drag,
                Mass = _shipData.Mass,
                Force = _shipData.Force,
                SpeedLimit = _shipData.SpeedLimit,
            };

            PhysicsWorld physicsWorld = _diContainer.Resolve<PhysicsWorld>();
            physicsWorld.Add(physicsBody);

            Ship.ShipView view = instance.GetComponent<Ship.ShipView>();
            Ship.Ship ship = new Ship.Ship(physicsWorld, _shipData, new Move.Move(physicsBody, _shipData.SpeedRotate, _shipData.Force), physicsBody);

            instance.GetComponent<TransformView>().Init(physicsBody);

            view.Init(ship);

            return (view, ship);
        }
    }
}