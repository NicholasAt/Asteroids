using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IBulletFactory
    {
        PhysicsBody CreateBullet(Vector2 position, bool isSplit, float rotate, float speedLimit);
    }
}