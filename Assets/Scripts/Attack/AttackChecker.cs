using UnityEngine;

namespace Attack
{
    public class AttackChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask hitLayer;
        private EventBus eventBus;
        private void Awake()
        {
            eventBus = GetComponent<EventBus>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if ((hitLayer.value & (1 << other.gameObject.layer)) == 0)
                return;
            eventBus.OnColliderHitTrigger(other);
            eventBus.OnFinishTrigger();
        }
    }
}