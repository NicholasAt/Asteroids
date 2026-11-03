using Assets.Scripts.Data.Models.ShipProgress;
using System;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IProgressService
    {
        ShipProgress ShipProgress { get; }
        event Action<ShipProgress> OnShipProgressCreated;
        void SetShip(ShipProgress shipProgress);
    }
}