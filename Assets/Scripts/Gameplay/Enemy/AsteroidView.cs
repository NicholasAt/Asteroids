using UnityEngine;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class AsteroidView : MonoBehaviour
    {
        private Asteroid _asteroid;

        public void Init(Asteroid asteroid)
        {
            _asteroid = asteroid;
        }

        private void OnDestroy()
        {
            _asteroid.Destroy();
        }
    }
}