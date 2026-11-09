using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Configs.Ship;
using Assets.Game.Scripts.Gameplay.ApplyDamage;
using Assets.Game.Scripts.Gameplay.Common;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Game.Scripts.Gameplay.Move;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Ship;
using Assets.Game.Scripts.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services.Factories
{
    public class ShipFactory : IShipFactory
    {
        private readonly DiContainer _diContainer;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly PlayerSettings _playerSettings;
        private readonly SignalBus _signalBus;
        private readonly IProgressService _progressService;
        private readonly IWorldBounds _worldBounds;
        private readonly IInput _inputService;
        private readonly ITickService _tickService;

        public ShipFactory(DiContainer diContainer, PhysicsWorld physicsWorld, ShipData shipData, PlayerSettings playerSettings, SignalBus signalBus, IProgressService progressService, IWorldBounds worldBounds, IInput inputService, ITickService tickService)
        {
            _diContainer = diContainer;
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _playerSettings = playerSettings;
            _signalBus = signalBus;
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

                Radius = _playerSettings.Ship.Size,
                Drag = _playerSettings.Ship.Drag,
                Mass = _playerSettings.Ship.Mass,
                Force = _playerSettings.Ship.Force,
                AngularDrag = _playerSettings.Ship.AngularDrag,
                SpeedLimit = _playerSettings.Ship.SpeedLimit,
            };

            ShipApplyDamage applyDamage = new ShipApplyDamage(_progressService.ShipProgress, _playerSettings.InvulnerabilitySeconds);
            ShipMove move = new ShipMove(physicsBody, _inputService, _playerSettings.Ship.SpeedRotate, _playerSettings.Ship.Force);
            Ship.Ship ship = new(_shipData, move, physicsBody, _progressService.ShipProgress, applyDamage, _inputService);
            physicsBody.Owner = ship;

            _physicsWorld.Add(physicsBody);
            _worldBounds.AddInMap(ship);
            _tickService.Add(ship);

            applyDamage.OnDie += () =>
            {
                _physicsWorld.Remove(physicsBody);
                _worldBounds.Remove(ship);
                _tickService.Remove(ship);
                _signalBus.Fire<PlayerDeathSignal>();
            };

            ShipView view = instance.GetComponent<ShipView>();
            view.Init(_progressService.ShipProgress);
            instance.GetComponent<TransformView>().Init(physicsBody);

            return (physicsBody, view.transform, ship);
        }
    }
}