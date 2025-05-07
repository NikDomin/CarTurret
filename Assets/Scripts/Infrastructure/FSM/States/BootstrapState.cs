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
            await gameStateMachine.Enter<LoadLevelState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}