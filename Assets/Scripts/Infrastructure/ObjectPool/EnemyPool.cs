using Enemy.FSM;
using UnityEngine;
using Zenject;

namespace Infrastructure.ObjectPool
{
    public class EnemyPool : MonoMemoryPool<Transform, EnemyController>
    {
        protected override void Reinitialize(Transform player, EnemyController enemy)
        {
            enemy.Init(player);
        }

        protected override void OnDespawned(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        protected override void OnSpawned(EnemyController enemy)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}