using Assets.Game.Scripts.Core.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Signals;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Services
{
    public class TickService : MonoBehaviour, IInitializable, ITickService
    {
        private const int InitialTickablesCapacity = 32;

        private readonly List<ICustomTick> _tickables = new List<ICustomTick>(InitialTickablesCapacity);
        private PhysicsWorld _physicsWorld;
        private List<ICustomTick> _initTickables;
        private SignalBus _signalBus;
        private bool _pause;

        [Inject]
        private void Construct(PhysicsWorld physicsWorld, SignalBus signalBus, List<ICustomTick> initTickables)
        {
            _physicsWorld = physicsWorld;
            _initTickables = initTickables;
            _signalBus = signalBus;
            _signalBus.Subscribe<PlayerDeathSignal>(SetPause);
        }

        public void Initialize()
        {
            _initTickables.ForEach(_tickables.Add);
        }

        public void Add(ICustomTick tickable)
        {
            _tickables.Add(tickable);
        }
        public void Remove(ICustomTick tickable)
        {
            CollectionExtension.RemoveFrom(_tickables, tickable);
        }
        private void SetPause()
        {
            _pause = true;
        }

        private void Update()
        {
            if (_pause)
                return;

            float deltaTime = Time.deltaTime;

            _physicsWorld.Update(deltaTime);
            for (int i = _tickables.Count - 1; i >= 0; i--)
            {
                _tickables[i].Tick(deltaTime);
            }
        }
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PlayerDeathSignal>(SetPause);
        }
    }
}