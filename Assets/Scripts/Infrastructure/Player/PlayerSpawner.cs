using System.Collections.Generic;
using Movement;
using UnityEngine;
using Zenject;

namespace Infrastructure.Player
{
    public class PlayerSpawner : IInitializable, IRespawnable
    {
        private readonly DiContainer container;
        private readonly GameObject carPrefab;
        private readonly Transform startPoint;
        private readonly List<IPlayerTargetReceiver> targetReceivers;
        private GameObject currentPlayer;
        
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
            SpawnPlayer();
        }
        
        private void SpawnPlayer()
        {
            var playerInstance = container.InstantiatePrefabForComponent<TurretMovement>(
                carPrefab,
                startPoint.position,
                Quaternion.identity,
                null);

            currentPlayer = playerInstance.gameObject;

            foreach (var receiver in targetReceivers)
                receiver.SetPlayerTarget(playerInstance.transform);
        }
        
        public void Respawn()
        {
            if (currentPlayer != null)
                GameObject.Destroy(currentPlayer);

            SpawnPlayer();
        }
    }

    public interface IRespawnable
    {
        public void Respawn();
    }
}