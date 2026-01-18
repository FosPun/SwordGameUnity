namespace _Resources.Scripts.States.SimpleEnemy
{
    public class DeadState : IState
    {
        private Enemy _enemy;

        public DeadState(Enemy meleeEnemy)
        {
            this._enemy = meleeEnemy;
        }
        public void Enter()
        {
            _enemy.navMeshAgent.isStopped = true;
            _enemy.animator.SetBool("isDead", true);
            _enemy.collider.enabled = false;
        }

        public void Execute()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}