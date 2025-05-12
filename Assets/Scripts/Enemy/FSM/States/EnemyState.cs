using UnityEngine;

namespace Enemy.FSM.States
{
    public abstract class EnemyState
    {
        private readonly EnemyController controller;
        private readonly Animator animator;
        
        protected EnemyState(EnemyController controller, Animator animator)
        {
            this.controller = controller;
            this.animator = animator;
        }
        
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}