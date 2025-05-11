using Cinemachine;
using Infrastructure.Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraHandler : MonoBehaviour, IPlayerTargetReceiver, IRespawnable
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

        public void Respawn()
        {
            followCamera.enabled = false;
            followCamera.transform.position = new Vector3(0, 11, -15);
            followCamera.transform.rotation = Quaternion.Euler(new Vector3(36, 0, 0));
            followCamera.enabled = true;
            sideCamera.Priority = 20;
            followCamera.Priority = 10;
        }
    }
}