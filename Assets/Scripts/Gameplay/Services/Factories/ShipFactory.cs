using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory
    {
        private readonly DiContainer _diContainer;
        private readonly SignalBus _signalBus;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly IPlayerPosition _playerPosition;
        private readonly IWorldBounds _worldBounds;
        private readonly IInput _inputService;

        public ShipFactory(DiContainer diContainer, SignalBus signalBus, PhysicsWorld physicsWorld, ShipData shipData, IPlayerPosition playerPosition, IWorldBounds worldBounds, IInput inputService)
        {
            _diContainer = diContainer;
            _signalBus = signalBus;
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _playerPosition = playerPosition;
            _worldBounds = worldBounds;
            _inputService = inputService;
        }

        public (Ship.ShipView, Ship.Ship) Create()
        {
            GameObject instance = _diContainer.InstantiatePrefab(_shipData.ShipPrefab);
            instance.ToMoveToActiveScene();

            PhysicsBody physicsBody = new PhysicsBody()
            {
                Layer = CollisionLayer.Player,
                CollidesWith = CollisionLayer.Enemy,

                Drag = _shipData.Drag,
                Mass = _shipData.Mass,
                Force = _shipData.Force,
                SpeedLimit = _shipData.SpeedLimit,
            };

            Ship.ShipView view = instance.GetComponent<Ship.ShipView>();
            Move.Move move = new Move.Move(physicsBody, _inputService, _shipData.SpeedRotate, _shipData.Force);
            Ship.Ship ship = new Ship.Ship(_physicsWorld, _signalBus, _shipData, new Health(_shipData.StartHealth), move, physicsBody, _playerPosition, _worldBounds);
            physicsBody.Owner = ship;

            _physicsWorld.Add(physicsBody);
            _worldBounds.AddInMap(ship);

            instance.GetComponent<TransformView>().Init(physicsBody);

            view.Init(ship);

            return (view, ship);
        }
    }
}