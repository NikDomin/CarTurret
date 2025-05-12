using Camera;
using Cysharp.Threading.Tasks;
using Infrastructure.Player;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class GameLoopState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;
        private UniTaskCompletionSource signalReceived;

        public GameLoopState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }
        
        public async UniTask Enter()
        {
            Debug.Log("Enter Game Loop State");
            signalBus.Fire<StartCameraFollowSignal>();
            signalBus.Fire<StartGameLoopSignal>();
            
            signalReceived = new UniTaskCompletionSource();
            await WaitLevelEnd();
            await gameStateMachine.Enter<LevelEndState>();
        }

        private async UniTask WaitLevelEnd()
        {
            signalBus.Subscribe<StopGameLoopSignal>(OnSignal);
            await signalReceived.Task;
            signalBus.Unsubscribe<StopGameLoopSignal>(OnSignal);
        }

        private void OnSignal() => signalReceived.TrySetResult();
        public UniTask Exit() => UniTask.CompletedTask;
    }
}