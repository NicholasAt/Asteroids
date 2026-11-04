using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Models.ShipProgress;
using System;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IProgressService
    {
        ShipProgress ShipProgress { get; }
        ScoreProgress ScoreProgress { get; }

        void SetScore(ScoreProgress score);
        void SetShip(ShipProgress shipProgress);
    }
}