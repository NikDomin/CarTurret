using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Level.Finish
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private LayerMask hitLayer;
        private SignalBus signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if ((hitLayer.value & (1 << other.gameObject.layer)) == 0)
                return;
            
            Debug.Log("Victory! Player reached the finish line.");
            signalBus.Fire<StopGameLoopSignal>();
            signalBus.Fire<WinSignal>();
            
        }
    }
}