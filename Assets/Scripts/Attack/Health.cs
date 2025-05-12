using System;
using UnityEngine;

namespace Attack
{
    public class Health : MonoBehaviour
    {
        public event Action<float> OnChange;
        public event Action OnDead;
        [SerializeField]
        private float currentHealth, maxHealth;

        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }
            
        public void GetHit(float amount)
        {
            currentHealth -= amount;
            if (currentHealth < 0)
                currentHealth = 0;
            
            OnChange?.Invoke(currentHealth);
            if (currentHealth <= 0)
                OnDead?.Invoke();
        }
    }
}