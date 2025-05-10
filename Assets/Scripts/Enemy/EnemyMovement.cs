using UnityEngine;

namespace Enemy
{
    public class EnemyMovement
    {
        private Rigidbody rb;
        private Transform enemyTransform;
        private readonly float rotationSpeed;

        public EnemyMovement(Rigidbody rb, Transform enemyTransform, float rotationSpeed)
        {
            this.rb = rb;
            this.enemyTransform = enemyTransform;
            this.rotationSpeed = rotationSpeed;
        }

        public void MoveTo(Vector3 target, float speed)
        {
            Vector3 direction = (target - enemyTransform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                Quaternion newRotation = Quaternion.RotateTowards(
                        enemyTransform.rotation,
                        targetRotation,
                        rotationSpeed * Time.fixedDeltaTime
                    );
                rb.MoveRotation(newRotation);
            }

            Vector3 forwardMovement = enemyTransform.forward * (speed * Time.fixedDeltaTime);
            Vector3 newPosition = enemyTransform.position + forwardMovement;

            rb.MovePosition(newPosition);
            
            
            // Vector3 direction = (target - enemyTransform.position).normalized;
            // Vector3 nextPosition = enemyTransform.position + direction * (speed * Time.fixedDeltaTime);
            // rb.MovePosition(nextPosition);
        }
        
        public Vector3 GetRandomPatrolPoint()
        {
            Vector3 randomDirection = Random.insideUnitCircle;
            randomDirection.y = 0f;
            return enemyTransform.position + randomDirection * 5f;
        }
    }
}