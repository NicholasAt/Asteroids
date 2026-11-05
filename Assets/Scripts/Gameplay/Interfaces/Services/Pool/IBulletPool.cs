using Assets.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services.Pool
{
    public interface IBulletPool
    {
        ILaunched Get(Vector2 position, float rotate, bool isSplit, float force, float speedLimit);
    }
}