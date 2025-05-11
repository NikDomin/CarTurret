using Infrastructure.ObjectPool;
using Infrastructure.Player;
using Infrastructure.Signals;
using Level.Finish;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour, IPlayerTargetReceiver, IFinishLineReceiver, IRespawnable
    {
        [SerializeField] private float spawnTriggerDistance = 20f;
        [SerializeField] private float spawnOffset = 30f;
        [SerializeField] private float firstSpawnDistance = 60f;
        [SerializeField] private int minGroupSize = 5;
        [SerializeField] private int maxGroupSize = 10;
        [SerializeField] private float roadWidth = 3f;
        [SerializeField] private float groupSpread = 2f;

        private SignalBus signalBus;
        private EnemyPool enemyPool;
        private Transform playerTransform, finishLineTransform;
        private float lastSpawnZ;
        private bool isStopSpawn;
        [Inject]
        public void Construct(EnemyPool enemyPool, SignalBus signalBus)
        {
            this.enemyPool = enemyPool;
            this.signalBus = signalBus;
        }
        
        private void Start()
        {
            FirstSpawn();
            signalBus.Subscribe<LevelEndSignal>(StopSpawn);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<LevelEndSignal>(StopSpawn);
        }

        private void Update()
        {
            if (isStopSpawn) return;
            
            if (playerTransform.position.z + spawnTriggerDistance >= lastSpawnZ)
            {
                float nextSpawnZ = lastSpawnZ + spawnOffset;
                if (nextSpawnZ < finishLineTransform.position.z)
                {
                    SpawnGroupAt(nextSpawnZ);
                    lastSpawnZ = nextSpawnZ;
                }
            }
        }

        private void SpawnGroupAt(float zPosition)
        {
            Vector3 basePosition = new Vector3(0f, 0f, zPosition);
            int groupSize = Random.Range(minGroupSize, maxGroupSize);
            
            for (int i = 0; i < groupSize; i++)
            {
                SpawnEnemy(basePosition);
            }
        }

        private void SpawnEnemy(Vector3 basePosition)
        {
            var spawnPos = FindSpawnPosition(basePosition);
            var enemy = enemyPool.Spawn(playerTransform);
            enemy.transform.position = spawnPos;
            enemy.transform.rotation = Quaternion.identity;
        }

        private Vector3 FindSpawnPosition(Vector3 basePosition)
        {
            float offsetX = Random.Range(-roadWidth, roadWidth);
            float offsetZ = Random.Range(-groupSpread, groupSpread);
            Vector3 spawnPos = basePosition + new Vector3(offsetX, 0, offsetZ);
            return spawnPos;
        }

        public void SetPlayerTarget(Transform target)
        {
            playerTransform = target;
        }

        public void SetFinishLine(Transform target)
        {
            finishLineTransform = target;
        }

        private void FirstSpawn()
        {
            float nextSpawnZ = 0 + firstSpawnDistance;
            SpawnGroupAt(nextSpawnZ);
            lastSpawnZ = nextSpawnZ;
        }

        private void StopSpawn() => isStopSpawn = true;

        public void Respawn()
        {
           isStopSpawn = false;
           FirstSpawn();
        }
    }
}