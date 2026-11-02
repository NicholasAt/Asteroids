using Assets.Scripts.Core.Common;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Register
{
    public class EnemyRegistry : IEnemyRegistry
    {
        private readonly List<IEnemyBehaviour> _pursuing = new();
        private readonly List<IEnemyBehaviour> _notPursuing = new();
        public IReadOnlyList<IEnemyBehaviour> NotPursuing => _notPursuing;

        public void Add(bool isPursuing, IEnemyBehaviour enemy)
        {
            (isPursuing ? _pursuing : _notPursuing).Add(enemy);
        }

        public void Remove(IEnemyBehaviour enemy)
        {
            if (CollectionExtension.RemoveFrom(_pursuing, enemy))
                return;
            CollectionExtension.RemoveFrom(_notPursuing, enemy);
        }
        public void Tick()
        {
            UpdateTick(_pursuing);
            UpdateTick(_notPursuing);
        }

        private void UpdateTick(List<IEnemyBehaviour> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                list[i].Update(Time.deltaTime);
        }
    }
}