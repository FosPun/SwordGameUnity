using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnAttack;
    [SerializeField] private int damage;
    [SerializeField] private AttackArea attackArea;
    
    private InputAction attack;
    private bool attackTrigger;
    private bool isAttacking;
    
    
    void Awake()
    {
        attack = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
       GetInput();
       if (attackTrigger) {Attack();}
    }

    private void GetInput()
    {
        attackTrigger = attack.IsPressed();
    }
    private void Attack()
    {   
        if(isAttacking) return;
        OnAttack?.Invoke();
        StartCoroutine(Hit());
    }
//ToDo Add timings, cooldowns, animations 
    private IEnumerator Hit()
    {
        
        isAttacking = true;
        
        foreach (var damagables in attackArea.Damagables)
        {
          damagables.TakeDamage(damage);
        }
        yield return new WaitForSeconds(1f); 
        isAttacking = false;
    }
}
