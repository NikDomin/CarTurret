using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootStrapInstaller : MonoInstaller
    { 
        [SerializeField] private InputHandler inputHandlerPrefab;
        public override void InstallBindings()
        {
            BindFSM();
            BindInputService();
        }

        private void BindFSM()
        {
            Container
                .Bind<GameStateMachine>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<GameInitializer>()
                .AsSingle();
            
            Container.Bind<IGameState>().To<BootstrapState>().AsSingle();
            Container.Bind<IGameState>().To<LoadLevelState>().AsSingle();
            Container.Bind<IGameState>().To<GameLoopState>().AsSingle();
            Container.Bind<IGameState>().To<LevelReadyState>().AsSingle();
        }

        private void BindInputService()
        {
            Container
                .Bind<IInputService>()
                .To<InputHandler>()
                .FromComponentInNewPrefab(inputHandlerPrefab)
                .AsSingle();
        }
        
    }
}