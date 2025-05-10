using UnityEngine;

namespace Enemy.FSM.States
{
    public class IdleState : EnemyState
    {
        private readonly EnemyController controller;
        private readonly Transform playerTransform;
        private readonly Animator animator;
        private readonly float rangeToDetectPlayer;
        private float waitTime;
        private readonly int idleTriggerHash;
        
        public IdleState(EnemyController controller, Animator animator, Transform playerTransform, float rangeToDetectPlayer, int idleTriggerHash) : base(controller, animator)
        {
            this.controller = controller;
            this.playerTransform = playerTransform;
            this.animator = animator;
            this.rangeToDetectPlayer = rangeToDetectPlayer;
            this.idleTriggerHash = idleTriggerHash;
        }

        public override void Enter()
        {
            animator.SetTrigger(idleTriggerHash);
            waitTime = Random.Range(4f, 8f);
        }

        public override void Exit()
        {
            animator.ResetTrigger(idleTriggerHash);
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