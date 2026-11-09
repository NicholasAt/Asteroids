namespace Assets.Game.Scripts.Core.Interfaces.States
{
    public interface IPayloadedState<TPayload>
    {
        void Enter(TPayload payload);
    }
}