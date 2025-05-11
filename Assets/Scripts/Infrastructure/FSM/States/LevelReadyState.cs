using Cysharp.Threading.Tasks;
using UI;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class LevelReadyState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;
        private UniTaskCompletionSource signalReceived;
        
        public LevelReadyState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }
        
        public async UniTask Enter()
        {
            signalReceived = new UniTaskCompletionSource();
            await WaitPlayButton();
            // signalBus.Fire<StartCameraFollowSignal>();
            
            await gameStateMachine.Enter<GameLoopState>();
        }

        private async UniTask WaitPlayButton()
        {
            signalBus.Subscribe<PlayButtonClickedSignal>(OnSignal);
            await signalReceived.Task;
            signalBus.Unsubscribe<PlayButtonClickedSignal>(OnSignal);
        }

        private void OnSignal()=> signalReceived.TrySetResult();
        

        public UniTask Exit() => UniTask.CompletedTask;
    }
}