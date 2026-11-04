using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Data.Configs.Constants;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _dontDestroyOnLoad;
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Start()
        {
            foreach (GameObject target in _dontDestroyOnLoad)
            {
                target.transform.parent = null;
                DontDestroyOnLoad(target);
            }
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConstants.LevelSceneKey);
        }
    }
}