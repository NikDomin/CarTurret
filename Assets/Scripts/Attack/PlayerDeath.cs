using DefaultNamespace;
using Infrastructure.Signals;
using UnityEngine;
using Zenject;

namespace Attack
{
    public class PlayerDeath : MonoBehaviour
    {
        private SignalBus signalBus;
        private Health health;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }
        private void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Start()
        {
            health.OnDead += PlayerDestroy;
        }

        private void OnDestroy()
        {
            health.OnDead -= PlayerDestroy;
        }

        private void PlayerDestroy()
        {
            signalBus.Fire<StopGameLoopSignal>();
            GameObject.Destroy(gameObject);
        }
    }
}