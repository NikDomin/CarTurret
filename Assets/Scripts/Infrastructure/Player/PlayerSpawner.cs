using Infrastructure.Player;
using Movement;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class PlayerSpawner : IInitializable
    {
        private readonly DiContainer container;
        private readonly GameObject carPrefab;
        private readonly Transform startPoint;
        private readonly IPlayerTargetReceiver playerTargetReceiver;
        
        public PlayerSpawner(
            DiContainer container,
            GameObject carPrefab,
            Transform startPoint,
            IPlayerTargetReceiver playerTargetReceiver)
        {
            this.container = container;
            this.carPrefab = carPrefab;
            this.startPoint = startPoint;
            this.playerTargetReceiver = playerTargetReceiver;
        }

        public void Initialize()
        {
            var playerInstance = container
                .InstantiatePrefabForComponent<TurretMovement>(
                carPrefab,
                startPoint.position,
                Quaternion.identity,
                null);
 
            playerTargetReceiver.SetPlayerTarget(playerInstance.transform);

        }
    }
}