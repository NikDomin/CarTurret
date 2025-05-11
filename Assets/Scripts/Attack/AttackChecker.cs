using UnityEngine;

namespace Attack
{
    public class AttackChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask hitLayer;
        private EventBus eventBus;
        // private Collider collider;
        private void Awake()
        {
            eventBus = GetComponent<EventBus>();
            // collider = GetComponent<Collider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if ((hitLayer.value & (1 << other.gameObject.layer)) == 0)
                return;
            eventBus.ONColliderHitTrigger(other);
            eventBus.OnFinishTrigger();
        }
    }
}