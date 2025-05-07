using System;
using UnityEngine;

namespace Shooting.Projectile
{
    public class ProjectileEventBus : MonoBehaviour
    {
        public event Action OnFinish;
        public event Action<Collider> OnColliderHit;

        public void Init(Action onFinish)
        {
            OnFinish = onFinish;
        }
        
        public void OnFinishTrigger() => OnFinish?.Invoke();
        public void ONColliderHitTrigger(Collider collider) => OnColliderHit?.Invoke(collider);
    }
}