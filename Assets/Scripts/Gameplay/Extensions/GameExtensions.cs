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
    }
}