using Attack;
using UnityEngine;

namespace Enemy
{
    public class DamageReceiver : MonoBehaviour, IDamageable
    {
        private Health health;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        public void Damage(float amount) => health.GetHit(amount);
        
    }
}