namespace Assets.Scripts.Core.Bootstrap.States
{
    public interface IPayloadedState<TPayload>
    {
        void Enter(TPayload payload);
    }
}