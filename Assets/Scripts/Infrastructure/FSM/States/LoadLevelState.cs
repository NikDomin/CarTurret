using Cysharp.Threading.Tasks;
using Level;
using UnityEngine.SceneManagement;

namespace Infrastructure.FSM.States
{
    public class LoadLevelState : IGameState
    {
        private readonly GameStateMachine gameStateMachine;

        public LoadLevelState(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public async UniTask Enter()
        {
            await SceneManager.LoadSceneAsync(Scenes.GAMESCENE);
            await gameStateMachine.Enter<LevelReadyState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}