using UnityEngine;

namespace Attack.Shooting
{
    [RequireComponent(typeof(LineRenderer))]
    public class TurretLineRenderer : MonoBehaviour
    {
        [SerializeField] private Transform firePoint;
        [SerializeField] private float maxDistance = 100f;
        [SerializeField] private LayerMask hitLayers;

        private LineRenderer lineRenderer;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
        }

        void Update()
        {
            Vector3 origin = firePoint.position;
            Vector3 direction = firePoint.forward;
            Vector3 endPoint = origin + direction * maxDistance;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitLayers))
            {
                endPoint = hit.point;
            }

            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}