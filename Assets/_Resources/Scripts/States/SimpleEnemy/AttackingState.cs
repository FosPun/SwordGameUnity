using UnityEngine;

namespace _Resources.Scripts.States.SimpleEnemy
{
    public class AttackingState : IState
    {
        private Enemy enemy;

        public AttackingState(Enemy enemy)
        {
            this.enemy = enemy;
        }
        public void Enter()
        {
            Debug.Log(enemy.gameObject.name + " is attacking");
            enemy.transform.LookAt(enemy.target.transform);
            enemy.Attack();
        }

        public void Execute()
        {
            if (!enemy.isAttacking)
            {
                enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.AttackingState);
            }
        }

        
        public void Exit()
        {
            
        }
    }
}