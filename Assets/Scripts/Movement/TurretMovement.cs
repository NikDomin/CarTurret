using System;
using Input;
using UnityEngine;

namespace Movement
{
    public class TurretMovement : MonoBehaviour
    {
        [SerializeField] private InputHandler inputHandler;
        
        private Vector2 inputVector;
        private Camera camera;
        
        #region Mono

        private void Awake()
        {
            camera = Camera.main;
        }

        private void OnEnable()
        {
            inputHandler.OnScreenPosition += SetInput;
        }

        private void OnDisable()
        {
            inputHandler.OnScreenPosition -= SetInput;
        }

        private void SetInput(Vector2 inputVector)
        {
            this.inputVector = inputVector;
        }

        private void FixedUpdate()
        {
            // RotateTurret();
            RotateTowardsMouse();
        }

        #endregion
        
        private void RotateTowardsMouse()
        {  
            Ray ray = camera.ScreenPointToRay(inputVector);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                Vector3 lookTarget = hitInfo.point;
                Vector3 direction = lookTarget - transform.position;
                direction.y = 0f; // Игнорируем высоту, только поворот по Y

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = targetRotation;
                }
            }
        }
    }
}