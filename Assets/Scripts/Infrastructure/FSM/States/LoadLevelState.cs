using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
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
            Debug.Log("Load game scene");
            var loadOp = SceneManager.LoadSceneAsync(Scenes.GAMESCENE);
            await loadOp;
            await UniTask.NextFrame();
            await gameStateMachine.Enter<LevelReadyState>();
        }

        public UniTask Exit() => UniTask.CompletedTask;
    }
}