using Camera;
using Enemy;
using Enemy.FSM;
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
        
        [Header("Enemy")]
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private int initEnemySize;
        [SerializeField] private GameObject enemyPrefab;
        
        public override void InstallBindings()
        {
            BindUI();
            BindProjectilePool();
            BindPlayer();
            BindCameraHandler();
            BindEnemySpawner();
        }

        private void BindEnemySpawner()
        {
            Container
                .BindInterfacesTo<EnemySpawner>()
                .FromInstance(enemySpawner)
                .AsSingle();
            
            Container.BindMemoryPool<EnemyController, EnemyPool>()
                .WithInitialSize(initEnemySize)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Enemies");
        }

        private void BindCameraHandler()
        {
            Container
                .BindInterfacesTo<CameraHandler>()
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
                .WithArguments(CarPrefab, StartPoint);
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