using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Register
{
    public class EnemyRegistry : IEnemyRegistry
    {
        private readonly List<IEnemyBehaviour> _enemies = new();

        public void Add(IEnemyBehaviour enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(IEnemyBehaviour enemy)
        {
            _enemies.Remove(enemy);
        }

        public void Tick()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                _enemies[i].Update(Time.deltaTime);
            }
        }
    }
}