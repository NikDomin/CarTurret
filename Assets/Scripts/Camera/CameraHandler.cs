using Cinemachine;
using Infrastructure.Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraHandler : MonoBehaviour, IPlayerTargetReceiver
    {
        [SerializeField] private CinemachineVirtualCamera sideCamera;
        [SerializeField] private CinemachineVirtualCamera followCamera;

        private SignalBus signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            this.signalBus.Subscribe<StartCameraFollowSignal>(OnStartFollow);
            sideCamera.Priority = 20;
            followCamera.Priority = 10;
        }
        
        private void OnDisable()
        {
            signalBus.Unsubscribe<StartCameraFollowSignal>(OnStartFollow);
        }

        private void OnStartFollow()
        {
            followCamera.Priority = 20;
            sideCamera.Priority = 10;
        }

        public void SetPlayerTarget(Transform target)
        {
            followCamera.Follow = target;
            followCamera.LookAt = target;
        }
    }
}