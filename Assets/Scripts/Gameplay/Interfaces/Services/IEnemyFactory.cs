using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyFactory
    {
        void CreateAsteroid(Vector2 position, Vector2 direction, float force, float speedLimit, float drag);
        void CreateFragment(Vector2 position, Vector2 direction, float force, float speedLimit, float drag);
    }
}