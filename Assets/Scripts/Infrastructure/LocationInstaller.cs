using Infrastructure.ObjectPool;
using Movement;
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
        
        public override void InstallBindings()
        {
            BindProjectilePool();
            BindPlayer();
        }
        
        private void BindPlayer()
        {
            TurretMovement turretMovement = Container
                .InstantiatePrefabForComponent<TurretMovement>(CarPrefab, StartPoint.position, Quaternion.identity, null);
            
            //not used
            // Container
            //     .Bind<TurretMovement>()
            //     .FromInstance(turretMovement)
            //     .AsSingle();
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