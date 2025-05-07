using UnityEngine;

namespace Shooting.Projectile
{
    public class ProjectileAttackChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask hitLayer;
        private ProjectileEventBus eventBus;
        // private Collider collider;
        private void Awake()
        {
            eventBus = GetComponent<ProjectileEventBus>();
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