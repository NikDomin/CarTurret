using System.Collections.Generic;
using Enemy.FSM;
using Infrastructure.Player;
using UnityEngine;
using Zenject;

namespace Infrastructure.ObjectPool
{
    public class EnemyPool : MonoMemoryPool<Transform, EnemyController>, IRespawnable
    {
        private List<EnemyController> activeEnemies = new();

        protected override void Reinitialize(Transform player, EnemyController enemy)
        {
            enemy.Init(player);
        }

        protected override void OnDespawned(EnemyController enemy)
        {
            enemy.gameObject.SetActive(false);
            activeEnemies.Remove(enemy);
        }

        protected override void OnSpawned(EnemyController enemy)
        {
            enemy.gameObject.SetActive(true);
            activeEnemies.Add(enemy);
        }

        public void Respawn()
        {
            foreach (var enemy in activeEnemies.ToArray())
            {
                Despawn(enemy);
            }
        }
    }
}