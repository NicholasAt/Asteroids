using Assets.Game.Scripts.Data.Models;
using Assets.Game.Scripts.Data.Models.ShipProgress;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services
{
    public interface IProgressService
    {
        ShipProgress ShipProgress { get; }
        ScoreProgress ScoreProgress { get; }

        void SetScore(ScoreProgress score);
        void SetShip(ShipProgress shipProgress);
    }
}