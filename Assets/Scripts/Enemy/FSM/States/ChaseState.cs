
using UnityEngine;

namespace Enemy.FSM.States
{
    public class ChaseState : EnemyState
    {
        private readonly Transform playerTransform;
        private readonly EnemyMovement enemyMovement;
        private readonly Animator animator;
        private readonly float rangeToStopChasePlayer;
        private readonly float speed;
        private readonly EnemyController controller;
        private readonly int runTriggerHash;
        
        public ChaseState(EnemyController controller, Animator animator, Transform playerTransform, EnemyMovement enemyMovement, float rangeToStopChasePlayer, float speed, int runTriggerHash) : base(controller, animator)
        {
            this.playerTransform = playerTransform;
            this.enemyMovement = enemyMovement;
            this.animator = animator;
            this.rangeToStopChasePlayer = rangeToStopChasePlayer;
            this.speed = speed;
            this.controller = controller;
            this.runTriggerHash = runTriggerHash;
        }

        public override void Enter()
        {
            animator.SetTrigger(runTriggerHash);
        }
        
        public override void Exit()
        {
            animator.ResetTrigger(runTriggerHash);
        }
        
        public override void FixedUpdate()
        {
            if (GetRangeToPlayer() > rangeToStopChasePlayer)
            {
                controller.StateMachine.ChangeState(controller.IdleState);
            }
            
            enemyMovement.MoveTo(playerTransform.position, speed);
        }
        
        private float GetRangeToPlayer()
        {
            return Vector3.Distance(controller.transform.position, playerTransform.position);
        }
    }
}