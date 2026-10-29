using Assets.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private IEnemyBehaviour _enemy;

        public void Init(IEnemyBehaviour enemy)
        {
            _enemy = enemy;
            _enemy.OnDestroy += Destroy;
        }

        private void Destroy()
        {
            _enemy.OnDestroy -= Destroy;
            Destroy(gameObject);
        }
    }
}