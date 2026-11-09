using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.Core.Services
{
    public class SceneLoader
    {
        public async UniTask Load(string sceneKey, Action onLoaded)
        {
            try
            {
                AsyncOperation operation = SceneManager.LoadSceneAsync(sceneKey);
                await operation.ToUniTask();
                onLoaded?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}