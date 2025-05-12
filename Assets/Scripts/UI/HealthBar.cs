using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    { 
        [SerializeField] private Health health;
        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            slider.maxValue = health.GetMaxHealth();
            HealthChange(health.GetMaxHealth());
        }

        private void Start()
        {
            slider.maxValue = health.GetMaxHealth();
            HealthChange(health.GetMaxHealth());
            health.OnChange += HealthChange;
        }
        
        private void OnDestroy()
        {
            health.OnChange -= HealthChange;
        }
        
        private void HealthChange(float hp)
        {
            slider.value = hp;
        }
    }
}