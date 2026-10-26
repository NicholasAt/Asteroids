using Assets.Scripts.Core.Services;
using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Services.Factories;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Core.Bootstrap.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly ShipBuilder _shipBuilder;

        public LoadLevelState(SceneLoader sceneLoader, ShipBuilder shipBuilder)
        {
            _sceneLoader = sceneLoader;
            _shipBuilder = shipBuilder;
        }

        public void Enter(string payload)
        {
            _sceneLoader.Load(payload, OnLoaded).Forget();
        }

        private void OnLoaded()
        {
            _shipBuilder.CreateShip(weaponId1: WeaponId.Laser, weaponId2: WeaponId.Gun);
        }
    }
}