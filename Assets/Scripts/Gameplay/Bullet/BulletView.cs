using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;
using IPoolable = Assets.Scripts.Gameplay.Interfaces.Common.IPoolable;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class BulletView : MonoBehaviour
    {

        private PhysicsBody _physicsBody;
        private IPoolable _poolable;
        private PhysicsWorld _physicsWorld;

        [Inject]
        private void Construct(PhysicsWorld physicsWorld)
        {
            _physicsWorld = physicsWorld;
        }

        public void Init(PhysicsBody physicsBody, IPoolable poolable)
        {
            _physicsBody = physicsBody;
            _poolable = poolable;
            _poolable.OnShow += OnShow;
            _poolable.OnHide += OnHide;
        }

        private void OnDestroy()
        {
            _poolable.OnShow -= OnShow;
            _poolable.OnHide -= OnHide;
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