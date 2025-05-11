using Camera;
using Infrastructure.FSM;
using Infrastructure.FSM.States;
using Infrastructure.Player;
using Infrastructure.Signals;
using Input;
using Level;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootStrapInstaller : MonoInstaller
    { 
        [SerializeField] private InputHandler inputHandlerPrefab;
        public override void InstallBindings()
        {
            InstallSignalBus();
            BindFSM();
            BindInputService();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayButtonClickedSignal>();
            Container.DeclareSignal<StartCameraFollowSignal>();
            Container.DeclareSignal<StartGameLoopSignal>();
            Container.DeclareSignal<LevelEndSignal>();
            Container.DeclareSignal<StopGameLoopSignal>();
            Container.DeclareSignal<LevelRestartSignal>();
            Container.DeclareSignal<LevelRestartButtonPressedSignal>();
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
            Container.Bind<IGameState>().To<LevelEndState>().AsSingle();
            Container.Bind<IGameState>().To<LevelRestartState>().AsSingle();
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