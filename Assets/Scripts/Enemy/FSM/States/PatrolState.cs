using UnityEngine;

namespace Enemy.FSM.States
{
    public class PatrolState : EnemyState
    {
        private readonly Transform playerTransform;
        private readonly float rangeToDetectPlayer;
        private readonly float speed;
        private readonly EnemyController controller;
        private readonly EnemyMovement enemyMovement;
        private readonly Animator animator;
        private Vector3 patrolTarget;
        private readonly int patrolTriggerHash;

        public PatrolState(EnemyController controller, Animator animator, Transform playerTransform, float rangeToDetectPlayer, float speed, EnemyMovement enemyMovement, int patrolTriggerHash) : base(controller, animator)
        {
            this.playerTransform = playerTransform;
            this.rangeToDetectPlayer = rangeToDetectPlayer;
            this.speed = speed;
            this.controller = controller;
            this.enemyMovement = enemyMovement;
            this.animator = animator;
            this.patrolTriggerHash = patrolTriggerHash;
        }

        public override void Enter()
        {
            animator.SetTrigger(patrolTriggerHash);
            patrolTarget = enemyMovement.GetRandomPatrolPoint();
        }
        
        public override void Exit()
        {
            animator.ResetTrigger(patrolTriggerHash);
        }
        
        public override void FixedUpdate()
        {
            if (getRangeToPlayer() < rangeToDetectPlayer)
            {
                controller.StateMachine.ChangeState(controller.ChaseState);
            }
            
            enemyMovement.MoveTo(patrolTarget, speed);

            if (Vector3.Distance(controller.transform.position, patrolTarget) <= 0.2f)
            {
                controller.StateMachine.ChangeState(controller.IdleState);
            }
            
        }
        private float getRangeToPlayer()
        {
            return Vector3.Distance(controller.transform.position, playerTransform.position);
        }
    }
}