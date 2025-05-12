using Camera;
using Enemy;
using Enemy.FSM;
using Infrastructure.ObjectPool;
using Infrastructure.Player;
using Infrastructure.Signals;
using Level;
using Level.Finish;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private GameObject CarPrefab;
        
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int projectilePreloadCount = 10;
        [SerializeField] private CameraHandler cameraHandler;
        [Header("UI")]
        [SerializeField] private PlayButtonHandler playButtonHandler;
        [SerializeField] private UIMenu uiMenu;
        
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
            BindSignals();
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
                .AsSingle();

            Container
                .BindInterfacesTo<UIMenu>()
                .FromInstance(uiMenu)
                .AsSingle();
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

        private void BindSignals()
        {
            Container.DeclareSignal<WinSignal>();
            Container.DeclareSignal<PlayerDeathSignal>();
        }
    }
}