using Input;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure
{
    public class BootStrapInstaller : MonoInstaller
    { 
        [SerializeField] private InputHandler inputHandlerPrefab;
        public override void InstallBindings()
        {
            BindInputService();
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