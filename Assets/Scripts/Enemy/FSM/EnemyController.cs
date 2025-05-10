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
        [SerializeField] private float patrolRange = 3f;
        [SerializeField] private float patrolSpeed = 1.5f;
        [SerializeField] private float turnSpeed = 5f;
        private Transform playerTransform;
        private EnemyMovement enemyMovement;
        public EnemyStateMachine StateMachine { get; private set; }
        
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
            IdleState = new IdleState(this, playerTransform, rangeToDetectPlayer);
            PatrolState = new PatrolState(this, playerTransform, patrolRange, patrolSpeed, enemyMovement,
                rangeToDetectPlayer);
            ChaseState = new ChaseState(this, playerTransform, rangeToStopChasePlayer, enemyMovement, chaseSpeed);
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