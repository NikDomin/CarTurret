
using UnityEngine;

namespace Enemy.FSM.States
{
    public class ChaseState : EnemyState
    {
        private readonly Transform playerTransform;
        private readonly EnemyMovement enemyMovement;
        private readonly float rangeToStopChasePlayer;
        private float speed;
        private readonly EnemyController controller;

        public ChaseState(EnemyController controller, Transform playerTransform, float rangeToStopChasePlayer, EnemyMovement enemyMovement, float speed) : base(controller)
        {
            this.playerTransform = playerTransform;
            this.enemyMovement = enemyMovement;
            this.rangeToStopChasePlayer = rangeToStopChasePlayer;
            this.speed = speed;
            this.controller = controller;
        }

        public override void FixedUpdate()
        {
            if (getRangeToPlayer() > rangeToStopChasePlayer)
            {
                //Maybe despawn enemy here
                controller.StateMachine.ChangeState(controller.IdleState);
            }
            
            enemyMovement.MoveTo(playerTransform.position, speed);
        }
        
        private float getRangeToPlayer()
        {
            return Vector3.Distance(controller.transform.position, playerTransform.position);
        }
    }
}