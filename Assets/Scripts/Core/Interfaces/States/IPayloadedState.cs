namespace Assets.Scripts.Core.Interfaces.States
{
    public interface IPayloadedState<TPayload>
    {
        void Enter(TPayload payload);
    }
}