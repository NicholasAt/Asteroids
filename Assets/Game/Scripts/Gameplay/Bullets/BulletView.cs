using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Bullets
{
    public class BulletView : MonoBehaviour
    {
        private ILaunched _bullet;

        public void Init(ILaunched bullet)
        {
            _bullet = bullet;
            _bullet.OnShow += OnShow;
            _bullet.OnHide += OnHide;
        }

        private void OnDestroy()
        {
            _bullet.OnShow -= OnShow;
            _bullet.OnHide -= OnHide;
        }

        private void OnHide()
        {
            gameObject.SetActive(false);
        }

        private void OnShow()
        {
            gameObject.SetActive(true);
        }
    }
}