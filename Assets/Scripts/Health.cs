using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Health : MonoBehaviour
    {
        public event Action OnChange;
        public event Action OnDead;
        [SerializeField]
        private float currentHealth, maxHealth;

        private void OnEnable()
        {
            currentHealth = maxHealth;
        }

        public void GetHit(float amount)
        {
            currentHealth -= amount;
            OnChange?.Invoke();
            Debug.Log("New health on object: " + gameObject.name + " is: " + currentHealth);
            
            if (currentHealth <= 0)
                OnDead?.Invoke();
        }
    }
}