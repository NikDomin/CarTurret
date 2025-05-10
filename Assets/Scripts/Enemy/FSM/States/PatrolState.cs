using UnityEngine;

namespace Enemy.FSM.States
{
    public class PatrolState : EnemyState
    {
        private readonly Transform playerTransform;
        private readonly float rangeToDetectPlayer;
        private readonly float patrolRange;
        private readonly float speed;
        private readonly EnemyController controller;
        private readonly EnemyMovement enemyMovement;
        private Vector3 patrolTarget;

        public PatrolState(EnemyController controller, Transform playerTransform, float patrolRange, float speed, EnemyMovement enemyMovement, float rangeToDetectPlayer) : base(controller)
        {
            this.playerTransform = playerTransform;
            this.rangeToDetectPlayer = rangeToDetectPlayer;
            this.patrolRange = patrolRange;
            this.speed = speed;
            this.controller = controller;
            this.enemyMovement = enemyMovement;
        }

        public override void Enter()
        {
            patrolTarget = enemyMovement.GetRandomPatrolPoint();
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