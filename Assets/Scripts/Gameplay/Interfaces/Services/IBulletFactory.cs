using Assets.Scripts.Gameplay.Physics;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IBulletFactory
    {
        PhysicsBody CreateBullet();
    }
}