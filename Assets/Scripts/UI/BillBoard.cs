using UnityEngine;

namespace UI
{
    public class BillBoard : MonoBehaviour
    {
        private void LateUpdate()
        {
            transform.LookAt(transform.position + UnityEngine.Camera.main.transform.forward);
        }
    }
}