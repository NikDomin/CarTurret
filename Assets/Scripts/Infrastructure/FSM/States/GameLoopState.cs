using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Infrastructure.FSM.States
{
    public class GameLoopState : IGameState
    {
        
        private readonly GameStateMachine gameStateMachine;

        public GameLoopState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }
        
        public async UniTask Enter()
        {
            Debug.Log("Enter Game Loop State");
            //spawn enemy, player move... 
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}