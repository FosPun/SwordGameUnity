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
        
        if(enemy.CalculateDistanceToTarget() < enemy.distanceToAttack)
        {
            
            enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.AttackingState);
        }
        if (enemy.CalculateDistanceToTarget() < enemy.DistanceForLostTarget)
        {
            enemy.navMeshAgent.SetDestination(enemy.target.transform.position);
        }
        else
        {
            enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.IdleState);
        }
    }
    
    public void Exit()
    {
        enemy.animator.SetBool("isMoving", false);
        enemy.navMeshAgent.ResetPath();
    }

    
}