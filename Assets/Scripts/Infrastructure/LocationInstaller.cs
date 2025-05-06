using Movement;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private GameObject CarPrefab;
        
        public override void InstallBindings()
        {
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
    }
}