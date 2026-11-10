using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Gameplay.Extensions
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
            return new(-Mathf.Sin(radian), Mathf.Cos(radian));
        }
    }
}