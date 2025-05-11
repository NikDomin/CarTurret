using Cysharp.Threading.Tasks;
using Infrastructure.Signals;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class LevelEndState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;
        private UniTaskCompletionSource signalReceived;


        public LevelEndState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }
        
        public async UniTask Enter()
        {
            signalBus.Fire<LevelEndSignal>();
            
            signalReceived = new UniTaskCompletionSource();
            await waitRestartButton();
            
            await gameStateMachine.Enter<LevelRestartState>();
        }

        private async UniTask waitRestartButton()
        {
            signalBus.Subscribe<LevelRestartButtonPressedSignal>(OnSignal);
            await signalReceived.Task;
            signalBus.Unsubscribe<LevelRestartButtonPressedSignal>(OnSignal);
        }

        private void OnSignal() => signalReceived.TrySetResult();

        public UniTask Exit() => UniTask.CompletedTask;
    }
}