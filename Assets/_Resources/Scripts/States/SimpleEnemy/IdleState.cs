using UnityEngine;

public class IdleState : IState
{
    private Enemy enemy;
    public IdleState(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Enter()
    {
       Debug.Log(enemy.gameObject.name + "Entered IdleState");
    }

    public void Execute()
    {
        if (enemy.CalculateDistanceToTarget() < enemy.enemySO.DistanceForFollowTarget && !enemy.targetHealth.isDead)
        {  
            enemy.EnemyStateMachine.Transition(enemy.EnemyStateMachine.FollowState);
        }
    }

    public void Exit()
    {
        
    }
}
