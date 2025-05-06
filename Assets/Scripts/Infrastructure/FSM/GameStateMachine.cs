using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Infrastructure.FSM
{
    public class GameStateMachine
    {
        private Dictionary<Type, IGameState> states;
        private IGameState currentState;

        public void Initialize(IEnumerable<IGameState> states)
        {
            this.states = states.ToDictionary(s => s.GetType(), s => s);
        }

        public async UniTask Enter<TState>() where TState : IGameState
        {
            if (currentState != null)
                await currentState.Exit();

            var newState = states[typeof(TState)];
            currentState = newState;
            await newState.Enter();
        }
    }
}