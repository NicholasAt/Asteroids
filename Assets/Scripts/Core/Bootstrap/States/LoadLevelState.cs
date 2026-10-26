using Assets.Scripts.Core.Services;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ShipFactory _shipFactory;

        public LoadLevelState(SceneLoader sceneLoader, ShipFactory shipFactory)
        {
            _sceneLoader = sceneLoader;
            _shipFactory = shipFactory;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _shipFactory.Create();
        }
    }
}