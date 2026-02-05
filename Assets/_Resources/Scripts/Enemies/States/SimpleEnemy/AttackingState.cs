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
            enemy.transform.LookAt(enemy.Target.transform);
        }

        public void Execute()
        {
            
            switch (enemy.CalculateDistanceToTarget() < enemy.enemySO.DistanceToAttack && !enemy.targetHealth.isDead)
            {
                case true:
                    enemy.Attack();
                    break;
                case false:
                    if(enemy.isAttacking) return;
                    enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.FollowState);
                    break;

            }
        }

        
        public void Exit()
        {
            Debug.Log(enemy.gameObject.name + " exit attacking");
        }
    }
}