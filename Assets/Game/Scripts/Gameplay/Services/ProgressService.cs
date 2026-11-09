using Assets.Game.Scripts.Data.Models;
using Assets.Game.Scripts.Data.Models.ShipProgress;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using System;

namespace Assets.Game.Scripts.Gameplay.Services
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