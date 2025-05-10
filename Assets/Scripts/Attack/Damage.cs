using System;
using Attack;
using UnityEngine;

namespace Shooting.Projectile
{
    [RequireComponent(typeof(EventBus))]
    public class Damage : MonoBehaviour
    {
        [SerializeField] private float damageAmount;
        private EventBus eventBus;

        private void Awake()
        {
            eventBus = GetComponent<EventBus>();
        }

        private void OnEnable()
        {
            eventBus.OnColliderHit += HandleDetectCollider;
        }

        private void OnDisable()
        {
            eventBus.OnColliderHit -= HandleDetectCollider;
        }

        private void HandleDetectCollider(Collider collider)
        {
            Debug.Log("Damage collider: " + collider.name);
            if(collider.TryGetComponent(out IDamageable damageable))
                damageable.Damage(damageAmount);
        }
    }

    internal interface IDamageable
    {
        public void Damage(float amount);
    }
}