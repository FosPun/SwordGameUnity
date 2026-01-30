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
        }

        public void Execute()
        {
            if (!enemy.isAttacking)
            {
                enemy.Attack();
            }

            if (enemy.CalculateDistanceToTarget() > enemy.distanceToAttack || enemy.targetHealth.isDead)
            {
                enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.FollowState);
            }
        }

        
        public void Exit()
        {
            
        }
    }
}