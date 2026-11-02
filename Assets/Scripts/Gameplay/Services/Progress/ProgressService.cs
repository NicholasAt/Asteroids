using Assets.Scripts.Data.Models.ShipProgress;
using Assets.Scripts.Gameplay.Interfaces.Services;

namespace Assets.Scripts.Gameplay.Services.Progress
{
    public class ProgressService: IProgressService
    {
        public ShipProgress ShipProgress { get; private set; }

        public void SetShip(ShipProgress shipProgress)
        {
            ShipProgress = shipProgress;
        }
    }
}