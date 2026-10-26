namespace Assets.Scripts.Gameplay.Physics
{
    public interface IPhysicsWorld
    {
        void Add(PhysicsBody physicsBody);
        void Remove(PhysicsBody physicsBody);
    }
}