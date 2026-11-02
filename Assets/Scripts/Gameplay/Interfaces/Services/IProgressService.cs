using Assets.Scripts.Data.Models.ShipProgress;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IProgressService
    {
        ShipProgress ShipProgress { get; }
        void SetShip(ShipProgress shipProgress);
    }
}