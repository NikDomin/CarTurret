using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Attack.Shooting
{
    public class ProjectileTimer : MonoBehaviour
    {
        [SerializeField] private float lifetime = 3f;
        private EventBus eventBus;
        private CancellationTokenSource lifetimeCts;
        private void Awake()
        {
            eventBus = GetComponent<EventBus>();
        }

        private void OnEnable()
        {
            lifetimeCts?.Cancel();
            lifetimeCts = new CancellationTokenSource();
            StartLifetimeTimer(lifetimeCts.Token).Forget();
        }

        private void OnDisable()
        {
            Deactivate();
        }
        
        private async UniTaskVoid StartLifetimeTimer(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(lifetime), cancellationToken: token);
            eventBus.OnFinishTrigger();
        }
        
        private void Deactivate()
        {
            lifetimeCts?.Cancel();
            lifetimeCts?.Dispose();
            lifetimeCts = null;
        }
    }
}