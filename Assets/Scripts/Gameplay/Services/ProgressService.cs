using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;
using System;

namespace Assets.Scripts.Gameplay.Services
{
    public class ProgressService : IProgressService
    {
        public ShipProgress ShipProgress { get; private set; }
        public event Action<ShipProgress> OnShipProgressCreated;
        public ScoreProgress ScoreProgress { get; private set; }

        public void SetScore(ScoreProgress score)
        {
            ScoreProgress = score;
        }

        public void SetShip(ShipProgress shipProgress)
        {
            ShipProgress = shipProgress;
            OnShipProgressCreated?.Invoke(shipProgress);
        }
    }
}