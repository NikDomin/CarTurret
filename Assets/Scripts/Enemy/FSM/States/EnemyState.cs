namespace Enemy.FSM.States
{
    public class EnemyState
    {
        private readonly EnemyController controller;
        
        protected EnemyState(EnemyController controller)
        {
            this.controller = controller;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}