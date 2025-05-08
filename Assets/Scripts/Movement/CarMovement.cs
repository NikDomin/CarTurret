using System;
using Infrastructure.Player;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Movement
{
    public class CarMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float forwardSpeed = 5f;

        [Header("Steering")]
        [SerializeField] private float maxSteeringAngle = 5f;
        [SerializeField] private float steeringChangeInterval = 2f;
        [SerializeField] private float steeringSmoothness = 2f;

        [Header("Road Constraints")]
        [SerializeField] private float roadMinX = -5f;
        [SerializeField] private float roadMaxX = 5f;
        [SerializeField] private float correctionAngle = 3f;

        private float targetAngleOffset = 0f;
        private float currentAngleOffset = 0f;
        private float timeToNextSteering;
        private Vector3 initialForward;
        private bool isMove = false;
        private SignalBus signalBus;
        
        private Rigidbody rb;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            this.signalBus = signalBus;
            signalBus.Subscribe<StartGameLoopSignal>(StartMove);
        }

        private void OnDisable()
        {
            signalBus.Unsubscribe<StartGameLoopSignal>(StartMove);
        }

        private void StartMove() => isMove = true;
        

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        private void Start()
        {
            initialForward = transform.forward.normalized;
            PickNewSteeringAngle();
        }

        private void FixedUpdate()
        {
            if(!isMove) 
                return;
            
            UpdateSteeringTimer();
            CheckAndCorrectBounds();
            Steer();
            MoveForward();
        }

        private void MoveForward()
        {
            Vector3 forwardDir = transform.forward;
            rb.MovePosition(rb.position + forwardDir * (forwardSpeed * Time.fixedDeltaTime));
        }

        private void Steer()
        {
            currentAngleOffset = Mathf.Lerp(currentAngleOffset, targetAngleOffset, Time.fixedDeltaTime * steeringSmoothness);

            Quaternion targetRotation = Quaternion.LookRotation(Quaternion.Euler(0, currentAngleOffset, 0) * initialForward);
            Quaternion smoothedRotation = Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * steeringSmoothness);

            rb.MoveRotation(smoothedRotation);
        }

        private void UpdateSteeringTimer()
        {
            timeToNextSteering -= Time.fixedDeltaTime;
            if (timeToNextSteering <= 0f)
            {
                PickNewSteeringAngle();
            }
        }

        private void PickNewSteeringAngle()
        {
            targetAngleOffset = Random.Range(-maxSteeringAngle, maxSteeringAngle);
            timeToNextSteering = Random.Range(steeringChangeInterval * 0.8f, steeringChangeInterval * 1.2f);
        }

        private void CheckAndCorrectBounds()
        {
            float x = transform.position.x;

            if (x < roadMinX + 1f)
            {
                targetAngleOffset = Mathf.Abs(correctionAngle);
            }
            else if (x > roadMaxX - 1f)
            {
                targetAngleOffset = -Mathf.Abs(correctionAngle);
            }
        }
    }
}