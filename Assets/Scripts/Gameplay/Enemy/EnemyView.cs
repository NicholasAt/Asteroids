using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private Enemy _enemy;
        private Saucer _move;

        public void Init(Enemy enemy)
        {
            _enemy = enemy;
            _enemy.OnDestroy += Destroy;
            if (_enemy is Saucer move)
                _move = move;
        }

        private void Update()
        {
            _move?.Update();
        }

        private void Destroy()
        {
            _enemy.OnDestroy -= Destroy;
            Destroy(gameObject);
        }
    }
}