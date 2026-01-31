using UnityEngine;

public class FollowState : IState
{
    private Enemy enemy;
    public FollowState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void Enter()
    {
        Debug.Log(enemy.gameObject.name + "is following");
        enemy.animator.SetBool("isMoving", true);
    }

    public void Execute()
    {
        
        if(enemy.CalculateDistanceToTarget() < enemy.enemySO.DistanceToAttack && !enemy.targetHealth.isDead)
        {
            
            enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.AttackingState);
        }
        else if  (enemy.targetHealth.isDead || enemy.CalculateDistanceToTarget() > enemy.enemySO.DistanceForLostTarget)
        {
            enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.IdleState);
        }
        else
        {
            enemy.navMeshAgent.SetDestination(enemy.Target.transform.position);

        }
        
        
      
    }
    
    public void Exit()
    {
        enemy.animator.SetBool("isMoving", false);
        enemy.navMeshAgent.ResetPath();
    }

    
}