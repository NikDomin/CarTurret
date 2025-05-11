using Cysharp.Threading.Tasks;
using Infrastructure.Signals;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class LevelEndState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;


        public LevelEndState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }
        
        public async UniTask Enter()
        {
            signalBus.Fire<LevelEndSignal>();
            await gameStateMachine.Enter<LevelRestartState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}