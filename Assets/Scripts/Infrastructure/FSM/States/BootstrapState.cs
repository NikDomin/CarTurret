using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.FSM.States
{
    public class BootstrapState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async UniTask Enter()
        {
            Debug.Log("Enter bootstrap scene");
            await UniTask.Delay(500); // эмуляция ожидания
            Debug.Log("awaited delay go to load level state");
            await gameStateMachine.Enter<LoadLevelState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}