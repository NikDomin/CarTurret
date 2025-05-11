using UnityEngine;

namespace Level.Finish
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private LayerMask hitLayer;
        
        private void OnTriggerEnter(Collider other)
        {
            if ((hitLayer.value & (1 << other.gameObject.layer)) == 0)
                return;
            
            Debug.Log("Victory! Player reached the finish line.");

        }
    }
}