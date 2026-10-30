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
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly IPlayerPosition _playerPosition;
        private readonly IWorldBounds _worldBounds;

        public ShipFactory(DiContainer diContainer, PhysicsWorld physicsWorld, ShipData shipData, IPlayerPosition playerPosition, IWorldBounds worldBounds)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _playerPosition = playerPosition;
            _worldBounds = worldBounds;
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
            Move.Move move = new Move.Move(physicsBody, _shipData.SpeedRotate, _shipData.Force);
            Ship.Ship ship = new Ship.Ship(_physicsWorld, _shipData, move, physicsBody, _playerPosition, _worldBounds);

            _physicsWorld.Add(physicsBody);
            _worldBounds.AddInMap(ship);

            instance.GetComponent<TransformView>().Init(physicsBody);

            view.Init(ship);

            return (view, ship);
        }
    }
}