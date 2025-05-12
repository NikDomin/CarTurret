using System.Collections;
using UnityEngine;

namespace Attack
{
    public class DamageFlash : MonoBehaviour
    {
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private Color flashColor = Color.red;
        [SerializeField] private float flashDuration = 0.2f;

        private Health health;
        private MaterialPropertyBlock propertyBlock;
        private static readonly int ColorProperty = Shader.PropertyToID("_Color");

        private void Awake()
        {
            health = GetComponent<Health>();
            propertyBlock = new MaterialPropertyBlock();
        }

        private void OnEnable()
        {
            health.OnChange += StartFlash;
        }

        private void OnDisable()
        {
            health.OnChange -= StartFlash;
        }

        public void Flash()
        {
            StartCoroutine(FlashRoutine());
        }
    
        private void StartFlash(float damage) => Flash();
    
        private IEnumerator FlashRoutine()
        {
            foreach (var rend in renderers)
            {
                rend.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor(ColorProperty, flashColor);
                rend.SetPropertyBlock(propertyBlock);
            }

            yield return new WaitForSeconds(flashDuration);

            foreach (var rend in renderers)
            {
                rend.GetPropertyBlock(propertyBlock);
                propertyBlock.SetColor(ColorProperty, Color.white);
                rend.SetPropertyBlock(propertyBlock);
            }
        }
    }
}