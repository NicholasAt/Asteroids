using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Bullets
{
    public class BulletView : MonoBehaviour
    {
        private ILaunched _bullet;
        private PhysicsBody _physicsBody;
        private PhysicsWorld _physicsWorld;

        [Inject]
        private void Construct(PhysicsWorld physicsWorld)
        {
            _physicsWorld = physicsWorld;
        }

        public void Init(PhysicsBody physicsBody, ILaunched bullet)
        {
            _bullet = bullet;
            _physicsBody = physicsBody;
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