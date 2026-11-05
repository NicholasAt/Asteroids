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
            _enemy.OnShow += Show;
            _enemy.OnDie += Hide;
        }

        private void OnDestroy()
        {
            _enemy.OnShow -= Show;
            _enemy.OnDie -= Hide;
        }
        private void Show()
        {
            gameObject.SetActive(true);
        }
        private void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}