using Camera;
using Enemy;
using Enemy.FSM;
using Infrastructure.ObjectPool;
using Infrastructure.Player;
using Level;
using Level.Finish;
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

        [Header("FinishLine")] 
        [SerializeField] private GameObject FinishLinePrefab;
        [SerializeField] private float finishLineSpawnDistance;
        
        public override void InstallBindings()
        {
            BindUI();
            BindProjectilePool();
            BindPlayer();
            BindCameraHandler();
            BindFinishLineSpawner();
            BindEnemySpawner();
            BindLevelRestart();
        }

        private void BindLevelRestart()
        {
            Container
                .BindInterfacesTo<LevelRespawn>()
                .AsSingle();
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
            
            Container.Bind<IRespawnable>()
                .To<EnemyPool>()
                .FromResolve()
                .AsCached();
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
                .Bind<PlayerSpawner>() 
                .AsSingle()
                .WithArguments(CarPrefab, StartPoint);

            Container
                .Bind<IInitializable>()
                .To<PlayerSpawner>()
                .FromResolve(); 

            Container
                .Bind<IRespawnable>()
                .To<PlayerSpawner>()
                .FromResolve();
        }

        private void BindFinishLineSpawner()
        {
            Container
                .Bind<IInitializable>()
                .To<FinishLineSpawner>()
                .AsSingle()
                .WithArguments(FinishLinePrefab, finishLineSpawnDistance);
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