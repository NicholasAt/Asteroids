using Cysharp.Threading.Tasks;

namespace Assets.Game.Scripts.Core.Interfaces.States
{
    public interface IPayloadedState<TPayload>
    {
        UniTask Enter(TPayload payload);
    }
}