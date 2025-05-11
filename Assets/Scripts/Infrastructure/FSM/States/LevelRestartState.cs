using Cysharp.Threading.Tasks;
using Level;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class LevelRestartState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;

        public LevelRestartState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }

        public async UniTask Enter()
        {
            signalBus.Fire<LevelRestartSignal>();
            await gameStateMachine.Enter<LevelReadyState>();
        }

        public UniTask Exit()=> UniTask.CompletedTask;
    }
}