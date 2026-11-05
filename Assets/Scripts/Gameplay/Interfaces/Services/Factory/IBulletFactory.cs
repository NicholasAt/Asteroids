using Assets.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IBulletFactory
    {
        ILaunched CreateBullet();
    }
}