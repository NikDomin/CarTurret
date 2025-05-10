using System;
using DefaultNamespace;
using Enemy.FSM;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyDisabler : MonoBehaviour
    {
        private Health health;
        private IMemoryPool pool;
        private EnemyController enemyController;
        [Inject]
        private void Construct(IMemoryPool pool)
        {
            this.pool = pool;
        }

        private void Awake()
        {
            health = GetComponent<Health>();
            enemyController = GetComponent<EnemyController>();
        }

        private void OnEnable()
        {
            health.OnDead += HandleDeath;
        }

        private void OnDisable()
        {
            health.OnDead -= HandleDeath;
        }

        private void HandleDeath()
        {
            pool.Despawn(enemyController);
        }
    }
}