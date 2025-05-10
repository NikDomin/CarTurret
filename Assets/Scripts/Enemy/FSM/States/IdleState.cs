using UnityEngine;

namespace Enemy.FSM.States
{
    public class IdleState : EnemyState
    {
        private readonly EnemyController controller;
        private readonly Transform playerTransform;
        private readonly float rangeToDetectPlayer;
        private float waitTime;

        public IdleState(EnemyController controller, Transform playerTransform, float rangeToDetectPlayer) : base(controller)
        {
            this.controller = controller;
            this.playerTransform = playerTransform;
            this.rangeToDetectPlayer = rangeToDetectPlayer;
        }

        public override void Enter()
        {
            waitTime = Random.Range(4f, 8f);
        }
        
        public override void Update()
        {
            waitTime -= Time.deltaTime;

            if (getRangeToPlayer() < rangeToDetectPlayer)
            {
                controller.StateMachine.ChangeState(controller.ChaseState);
            }
            else if (waitTime <= 0f)
            {
                controller.StateMachine.ChangeState(controller.PatrolState);
            }
        }

        private float getRangeToPlayer()
        {
            return Vector3.Distance(controller.transform.position, playerTransform.position);
        }
    }
}