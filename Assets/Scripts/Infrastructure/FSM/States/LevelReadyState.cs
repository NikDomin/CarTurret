using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.FSM.States
{
    public class LevelReadyState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;

        public LevelReadyState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async UniTask Enter()
        {
            //await until player not use play button for now just move to gameLoop
            await gameStateMachine.Enter<GameLoopState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}