using Input;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class TurretMovement : MonoBehaviour
    {
        private Vector2 inputVector;
        private UnityEngine.Camera _сamera;
        private IInputService inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            this.inputService = inputService;
            this.inputService.OnScreenPosition += SetInput;
        }
        
        #region Mono
        
        private void Awake()
        {
            _сamera = UnityEngine.Camera.main;
        }
        
        private void OnDisable()
        {
            inputService.OnScreenPosition -= SetInput;
        }
        
        private void FixedUpdate()
        {
            RotateTowardsMouse();
        }

        #endregion

        private void SetInput(Vector2 inputVector)
        {
            this.inputVector = inputVector;
        }

        private void RotateTowardsMouse()
        {  
            Ray ray = _сamera.ScreenPointToRay(inputVector);

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