using System;
using UnityEngine;

namespace Shooting.Projectile
{
    public class ProjectileMove : MonoBehaviour
    {
        public event Action onFinish;
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

