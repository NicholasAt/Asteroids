namespace Assets.Scripts.Core.Interfaces.States
{
    public interface IGameStateMachine
    {
        void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}