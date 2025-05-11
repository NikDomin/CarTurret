using UnityEngine;

namespace Enemy.FSM.States
{
    public class StopEnemyState : EnemyState
    {
        private readonly EnemyController controller;
        private readonly Animator animator;
        private readonly int idleTriggerHash;

        public StopEnemyState(EnemyController controller, Animator animator, int idleTriggerHash) : base(controller, animator)
        {
            this.controller = controller;
            this.animator = animator;
            this.idleTriggerHash = idleTriggerHash;
        }
        public override void Enter()
        {
            animator.SetTrigger(idleTriggerHash);
        }
        public override void Exit()
        {
            animator.ResetTrigger(idleTriggerHash);
        }

    }
}