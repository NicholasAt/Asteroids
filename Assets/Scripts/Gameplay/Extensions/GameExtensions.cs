using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Gameplay.Extensions
{
    public static class GameExtensions
    {
        public static void ToMoveToActiveScene(this GameObject target)
        {
            target.transform.parent = null;
            SceneManager.MoveGameObjectToScene(target, SceneManager.GetActiveScene());
        }
        public static Vector2 ToRotateToDirection(this float radian)
        {
            float rad = radian + (Mathf.Deg2Rad * 90);
            return new(Mathf.Cos(rad), Mathf.Sin(rad));
        }
    }
}