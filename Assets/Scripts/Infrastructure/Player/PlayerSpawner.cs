using System.Collections.Generic;
using Movement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Player
{
    public class PlayerSpawner : IInitializable
    {
        private readonly DiContainer container;
        private readonly GameObject carPrefab;
        private readonly Transform startPoint;
        private readonly List<IPlayerTargetReceiver> targetReceivers;
        
        public PlayerSpawner(
            DiContainer container,
            GameObject carPrefab,
            Transform startPoint,
            List<IPlayerTargetReceiver> targetReceivers)
        {
            this.container = container;
            this.carPrefab = carPrefab;
            this.startPoint = startPoint;
            this.targetReceivers = targetReceivers;
        }

        public void Initialize()
        {
            var playerInstance = container
                .InstantiatePrefabForComponent<TurretMovement>(
                carPrefab,
                startPoint.position,
                Quaternion.identity,
                null);
 
            foreach (var receiver in targetReceivers)
                receiver.SetPlayerTarget(playerInstance.transform);
        }
    }
}