using System;
using UnityEngine;

namespace Attack
{
    public class EventBus : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action<Collider> OnColliderHit;

        public void Init(Action onFinish)
        {
            OnFinish = onFinish;
        }
        
        public void OnFinishTrigger() => OnFinish?.Invoke();
        public void OnColliderHitTrigger(Collider collider) => OnColliderHit?.Invoke(collider);
    }
}