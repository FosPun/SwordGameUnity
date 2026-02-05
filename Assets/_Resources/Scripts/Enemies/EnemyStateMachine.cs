using System;
using System.Collections;
using _Resources.Scripts.States.SimpleEnemy;
using UnityEngine;

public class EnemyStateMachine 
{
    public IState CurrentState { get;private set; }

    public IdleState IdleState;
    public FollowState FollowState;
    public DeadState DeadState;
    public AttackingState AttackingState;
    
    public event Action<IState> OnStateChanged;

    public EnemyStateMachine(Enemy enemy)
    {
        this.IdleState = new IdleState(enemy);
        this.FollowState = new FollowState(enemy);
        this.DeadState = new DeadState(enemy);
        this.AttackingState = new AttackingState(enemy);
    }
    

    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
        OnStateChanged?.Invoke(state);
    }

    public void Transition(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
        
        OnStateChanged?.Invoke(nextState);
    }
    public void Execute()
    {
        if (CurrentState != null)
        {
            CurrentState.Execute();
        }
    }
}
