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
            signalBus.Subscribe<WinSignal>(OnWin);
        }

        private void OnDestroy()
        {
            health.OnDead -= PlayerDestroy;
            signalBus.Unsubscribe<WinSignal>(OnWin);

        }

        private void OnWin() => Destroy(gameObject);
        

        private void PlayerDestroy()
        {
            signalBus.Fire<StopGameLoopSignal>();
            signalBus.Fire<PlayerDeathSignal>();
            Destroy(gameObject);
        }
    }
}