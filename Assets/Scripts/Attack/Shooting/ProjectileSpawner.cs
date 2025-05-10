using Attack;
using DefaultNamespace.Shooting;
using Infrastructure.ObjectPool;
using Infrastructure.Player;
using Shooting.Projectile;
using UnityEngine;
using Zenject;

namespace Shooting
{
    public class ProjectileSpawner : MonoBehaviour, ISpawnable
    {
        [SerializeField] private float fireRate = 0.5f;
        [SerializeField] private Transform spawnPoint;
        
        private GameObjectPool projectilePool;
        private float nextFireTime;
        private SignalBus signalBus;
        private bool isStartSpawn;
        
        [Inject]
        public void Construct(SignalBus signalBus, GameObjectPool pool)
        {
            this.signalBus = signalBus;
            this.signalBus.Subscribe<StartGameLoopSignal>(StartSpawn);
            projectilePool = pool;
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<StartGameLoopSignal>(StartSpawn);
        }

        private void StartSpawn() => isStartSpawn = true;

        private void FixedUpdate()
        {
            if(!isStartSpawn)
                return;
            
            if (Time.fixedTime >= nextFireTime)
            {
                Spawn();
                nextFireTime = Time.fixedTime + fireRate;
            }
        }

        public void Spawn()
        {
            var projectile = projectilePool.Get();
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;

            if (projectile.TryGetComponent(out EventBus logic))
            {
                logic.Init(() => projectilePool.Return(projectile));
            }
        }
    }
}