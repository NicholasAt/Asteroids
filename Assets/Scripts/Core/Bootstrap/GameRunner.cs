using Assets.Scripts.Core.Bootstrap.States;
using Assets.Scripts.Data.Constants;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        private void Start()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(SceneConstants.LevelSceneKey);
        }
    }
}