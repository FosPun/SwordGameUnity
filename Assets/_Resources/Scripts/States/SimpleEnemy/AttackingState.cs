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
        }

        public void Execute()
        {
            if (enemy.targetHealth.isDead)
            {
                enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.IdleState);
            }
            enemy.Attack();
            if (enemy.CalculateDistanceToTarget() > enemy.distanceToAttack)
            {
                enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.FollowState);
            }
        }

        public void Exit()
        {
            
        }
    }
}