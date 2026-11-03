using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using System;

namespace Assets.Scripts.Gameplay.Services.Progress
{
    public class ProgressService : IProgressService
    {
        public ShipProgress ShipProgress { get; private set; }
        public event Action<ShipProgress> OnShipProgressCreated;

        public void SetShip(ShipProgress shipProgress)
        {
            ShipProgress = shipProgress;
            OnShipProgressCreated?.Invoke(shipProgress);
        }
    }
}