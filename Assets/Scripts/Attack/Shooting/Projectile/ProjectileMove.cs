using UnityEngine;

namespace Attack.Shooting.Projectile
{
    public class ProjectileMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        private void FixedUpdate()
        {
            rb.velocity = transform.forward * (speed * Time.fixedDeltaTime);
        }

      
    }
    
}

