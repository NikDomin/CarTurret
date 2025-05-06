using Input;
using Movement;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform StartPoint;
        [SerializeField] private GameObject CarPrefab;
        [FormerlySerializedAs("inputService")] [FormerlySerializedAs("InputHandler")] [SerializeField] private InputHandler inputHandler;
        
        public override void InstallBindings()
        {
            BindInputHandler();
            BindPlayer();
        }

        private void BindInputHandler()
        {
            Container
                .Bind<InputHandler>()
                .FromInstance(inputHandler)
                .AsSingle();
        }

        private void BindPlayer()
        {
            TurretMovement turretMovement = Container
                .InstantiatePrefabForComponent<TurretMovement>(CarPrefab, StartPoint.position, Quaternion.identity, null);
            //not used
            Container
                .Bind<TurretMovement>()
                .FromInstance(turretMovement)
                .AsSingle();
        }
    }
}