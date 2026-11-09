using Assets.Game.Scripts.Core.Interfaces.States;
using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Core.Bootstrap.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, object> _states;

        public GameStateMachine(LoadLevelState loadLevelState)
        {
            _states = new Dictionary<Type, object>
            {
                [typeof(LoadLevelState)] = loadLevelState
            };
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class
        {
            TState state = GetState<TState>();
            return state;
        }

        private TState GetState<TState>() where TState : class =>
          _states[typeof(TState)] as TState;
    }
}