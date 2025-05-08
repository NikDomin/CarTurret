using Camera;
using Infrastructure.ObjectPool;
using Infrastructure.Player;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private GameObject CarPrefab;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int projectilePreloadCount = 10;
        
        [SerializeField] private PlayButtonHandler playButtonHandler;
        [SerializeField] private CameraHandler cameraHandler;

        public override void InstallBindings()
        {
            BindUI();
            BindProjectilePool();
            BindPlayer();
            BindCameraHandler();
        }

        private void BindCameraHandler()
        {
            Container
                .Bind<IPlayerTargetReceiver>()  
                .To<CameraHandler>()            
                .FromInstance(cameraHandler)
                .AsSingle();
        }

        private void BindUI()
        { 
            Container
                .BindInterfacesTo<PlayButtonHandler>()
                .FromInstance(playButtonHandler)
                .AsSingle();;
        }

        private void BindPlayer()
        {
            Container
                .Bind<IInitializable>()
                .To<PlayerSpawner>()
                .AsSingle()
                .WithArguments(CarPrefab, StartPoint, cameraHandler);
        }
        
        private void BindProjectilePool()
        {
            var pool = new GameObjectPool(projectilePrefab, projectilePreloadCount);
    
            Container
                .Bind<GameObjectPool>()
                .FromInstance(pool)
                .AsSingle();
        }

    }
}