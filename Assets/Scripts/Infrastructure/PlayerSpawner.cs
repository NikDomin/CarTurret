using Movement;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PlayerSpawner : IInitializable
    {
        private readonly GameObject carPrefab;
        private readonly Transform startPoint;
        private readonly DiContainer container;

        public PlayerSpawner(
            DiContainer container,
            [Inject(Id = "StartPoint")] Transform startPoint,
            [Inject(Id = "CarPrefab")] GameObject carPrefab)
        {
            this.container = container;
            this.startPoint = startPoint;
            this.carPrefab = carPrefab;
        }

        public void Initialize()
        {
            var turretMovement = container.InstantiatePrefabForComponent<TurretMovement>(
                    carPrefab,
                    startPoint.position,
                    Quaternion.identity,
                    null
                );

            // Если кому-то нужен TurretMovement:
            container.Bind<TurretMovement>().FromInstance(turretMovement).AsSingle();
        }
    }
}