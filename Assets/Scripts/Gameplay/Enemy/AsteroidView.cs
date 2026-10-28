using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class AsteroidView : MonoBehaviour
    {
        private Asteroid _asteroid;

        public void Init(Asteroid asteroid)
        {
            _asteroid = asteroid;
            _asteroid.OnDestroy += Destroy;
        }

        private void Destroy()
        {
            _asteroid.OnDestroy -= Destroy;
            Destroy(gameObject);
        }
    }
}