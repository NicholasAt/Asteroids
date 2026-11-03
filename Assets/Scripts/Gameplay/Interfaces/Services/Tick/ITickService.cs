namespace Assets.Scripts.Gameplay.Interfaces.Services.Tick
{
    public interface ITickService
    {
        void Add(ICustomTick tickable);
        void Initialize();
        void Remove(ICustomTick tickable);
    }
}