using DefaultNamespace.Shooting;
using Infrastructure.ObjectPool;
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

        [Inject]
        public void Construct(GameObjectPool pool)
        {
            projectilePool = pool;
        }

        private void FixedUpdate()
        {
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

            if (projectile.TryGetComponent(out ProjectileEventBus logic))
            {
                logic.Init(() => projectilePool.Return(projectile));
            }
        }
    }
}