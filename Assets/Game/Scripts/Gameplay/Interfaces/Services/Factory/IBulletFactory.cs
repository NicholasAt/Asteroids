using Assets.Game.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IBulletFactory
    {
        ILaunched CreateBullet();
    }
}