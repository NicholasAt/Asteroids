using Assets.Scripts.Core.Common;
using Assets.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Scripts.Gameplay.Physics;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services
{
    public class TickService : MonoBehaviour, IInitializable, ITickService
    {
        private readonly List<ICustomTick> _tickables = new List<ICustomTick>(1000);
        private PhysicsWorld _physicsWorld;
        private List<ICustomTick> _initTickables;

        [Inject]
        private void Construct(PhysicsWorld physicsWorld, List<ICustomTick> initTickables)
        {
            _physicsWorld = physicsWorld;
            _initTickables = initTickables;
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
        private void Update()
        {
            float deltaTime = Time.deltaTime;

            _physicsWorld.Update(deltaTime);
            for (int i = _tickables.Count - 1; i >= 0; i--)
            {
                _tickables[i].Tick(deltaTime);
            }
        }
    }
}