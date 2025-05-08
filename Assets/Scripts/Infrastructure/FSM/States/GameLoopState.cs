using Camera;
using Cysharp.Threading.Tasks;
using Infrastructure.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure.FSM.States
{
    public class GameLoopState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;
        private readonly SignalBus signalBus;

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
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}