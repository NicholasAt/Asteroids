using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Core.Interfaces.States
{
    public interface IGameStateMachine
    {
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}