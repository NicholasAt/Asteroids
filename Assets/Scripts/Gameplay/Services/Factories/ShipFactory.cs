using Assets.Scripts.Data.Configs.Player;
using Assets.Scripts.Data.Configs.Ship;
using Assets.Scripts.Gameplay.ApplyDamage;
using Assets.Scripts.Gameplay.Common;
using Assets.Scripts.Gameplay.Extensions;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Scripts.Gameplay.Move;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Ship;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : IShipFactory
    {
        private readonly DiContainer _diContainer;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly PlayerData _playerData;
        private readonly IProgressService _progressService;
        private readonly IWorldBounds _worldBounds;
        private readonly IInput _inputService;
        private readonly ITickService _tickService;

        public ShipFactory(DiContainer diContainer, PhysicsWorld physicsWorld, ShipData shipData, PlayerData playerData, IProgressService progressService, IWorldBounds worldBounds, IInput inputService, ITickService tickService)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _playerData = playerData;
            _progressService = progressService;
            _worldBounds = worldBounds;
            _inputService = inputService;
            _tickService = tickService;
        }

        public (PhysicsBody, Transform, IWeaponKeeper) Create()
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
                AngularDrag = _shipData.AngularDrag,
                SpeedLimit = _shipData.SpeedLimit,
            };

            ShipApplyDamage applyDamage = new ShipApplyDamage(_progressService.ShipProgress, _playerData.InvulnerabilitySeconds);
            ShipMove move = new ShipMove(physicsBody, _inputService, _shipData.SpeedRotate, _shipData.Force);
            Ship.Ship ship = new Ship.Ship(_shipData, move, physicsBody, _progressService.ShipProgress, applyDamage, _inputService);
            physicsBody.Owner = ship;

            _physicsWorld.Add(physicsBody);
            _worldBounds.AddInMap(ship);
            _tickService.Add(ship);

            applyDamage.OnDie += () =>
            {
                _physicsWorld.Remove(physicsBody);
                _worldBounds.Remove(ship);
                _tickService.Remove(ship);
            };

            ShipView view = instance.GetComponent<ShipView>();
            view.Init(_progressService.ShipProgress);
            instance.GetComponent<TransformView>().Init(physicsBody);

            return (physicsBody, view.transform, ship);
        }
    }
}