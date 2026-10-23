using Assets.Scripts.Gameplay.Services.Factories;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Core.Infrastructure.Bootstrap
{
    public class GameRunner : MonoBehaviour
    {
        private ShipFactory _shipFactory;

        [Inject]
        private void Construct(ShipFactory shipFactory)
        {
            _shipFactory = shipFactory;
        }
        private void Start()
        {
            _shipFactory.Create();
        }
    }
}