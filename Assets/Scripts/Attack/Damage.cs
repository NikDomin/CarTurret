using UnityEngine;

namespace Attack
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
            if(collider.TryGetComponent(out IDamageable damageable))
                damageable.Damage(damageAmount);
        }
    }

    internal interface IDamageable
    {
        public void Damage(float amount);
    }
}