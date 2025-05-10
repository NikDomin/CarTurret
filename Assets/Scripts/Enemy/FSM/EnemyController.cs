using Enemy.FSM.States;
using UnityEngine;

namespace Enemy.FSM
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Chase")]
        [SerializeField] private float chaseSpeed = 3f;
        [SerializeField] private float rangeToStopChasePlayer = 15f;
        [SerializeField] private float rangeToDetectPlayer = 10f;
        [Header("Patrol")]
        // [SerializeField] private float patrolRange = 3f;
        [SerializeField] private float patrolSpeed = 1.5f;
        [SerializeField] private float turnSpeed = 5f;

        [SerializeField] private Animator animator;
        
        public EnemyStateMachine StateMachine { get; private set; }
        private Transform playerTransform;
        private EnemyMovement enemyMovement;
        private readonly int idleTriggerHash = Animator.StringToHash("IdleTrigger");
        private readonly int patrolTriggerHash = Animator.StringToHash("PatrolTrigger");
        private readonly int runTriggerHash = Animator.StringToHash("RunTrigger");
        
        #region States

        public IdleState IdleState { get; private set; }
        public PatrolState PatrolState { get; private set; }
        public ChaseState ChaseState { get; private set; }

        #endregion

        public void Init(Transform playerTransform)
        {
            this.playerTransform = playerTransform;
            enemyMovement = new EnemyMovement(GetComponent<Rigidbody>(), transform, turnSpeed);
            StateMachine = new EnemyStateMachine();
            CreateStates();
            StateMachine.Initialize(IdleState);
        }
        private void CreateStates()
        {
            IdleState = new IdleState(this, animator, playerTransform, rangeToDetectPlayer, idleTriggerHash);
            PatrolState = new PatrolState(this, animator, playerTransform,
                rangeToDetectPlayer,patrolSpeed, enemyMovement, patrolTriggerHash);
            ChaseState = new ChaseState(this, animator, playerTransform, 
                enemyMovement, rangeToStopChasePlayer, chaseSpeed, runTriggerHash);
        }

        private void Update()
        {
            StateMachine.CurrentState.Update();  
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.FixedUpdate();
        }
    }
}