using Infrastructure.Player;
using Infrastructure.Signals;
using Input;
using UI;
using UnityEngine;
using Zenject;

namespace Movement
{
    public class TurretMovement : MonoBehaviour
    {
        private Vector2 inputVector;
        private UnityEngine.Camera _сamera;
        private IInputService inputService;
        private SignalBus signalBus;
        private bool isRotation;
        [Inject]
        public void Construct(SignalBus signalBus, IInputService inputService)
        {
            this.signalBus = signalBus;
            this.inputService = inputService;
        }
        
        #region Mono
        
        private void Awake()
        {
            _сamera = UnityEngine.Camera.main;
        }

        private void Start()
        { 
            signalBus.Subscribe<StartGameLoopSignal>(StartRotation);
            signalBus.Subscribe<LevelEndSignal>(StopRotation);
            inputService.OnScreenPosition += SetInput;
        }

        private void OnDestroy()
        {
            isRotation = false;
            inputService.OnScreenPosition -= SetInput;
            signalBus.Unsubscribe<StartGameLoopSignal>(StartRotation);
            signalBus.Unsubscribe<LevelEndSignal>(StopRotation);
        }
        
        private void StopRotation() => isRotation = false;

        private void StartRotation() => isRotation = true;

        private void FixedUpdate()
        {
            if (!isRotation)
                return;
            
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
                direction.y = 0f;

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = targetRotation;
                }
            }
        }
    }
}