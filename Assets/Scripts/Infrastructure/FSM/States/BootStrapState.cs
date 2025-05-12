using Cysharp.Threading.Tasks;

namespace Infrastructure.FSM.States
{
    public class BootStrapState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;

        public BootStrapState(GameStateMachine gameStateMachine)
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