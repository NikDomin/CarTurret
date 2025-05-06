using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.FSM.States;
using Zenject;

namespace Infrastructure.FSM
{
    public class GameInitializer : IInitializable
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly IEnumerable<IGameState> states;

        public GameInitializer(GameStateMachine stateMachine, IEnumerable<IGameState> states)
        {
            gameStateMachine = stateMachine;
            this.states = states;
        }

        public void Initialize()
        {
            gameStateMachine.Initialize(states);
            gameStateMachine.Enter<BootstrapState>().Forget();
        }
    }
}