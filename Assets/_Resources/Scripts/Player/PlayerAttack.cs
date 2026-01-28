using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnAttack;
    
    [SerializeField] private int damage;
    [SerializeField] private AttackArea attackArea;
    
    private bool attackTrigger;
    private bool isAttacking;
    private InputAction attack;
    
    
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
        attackArea.Damage = damage;
        yield return new WaitForSeconds(1f); 
        isAttacking = false;
    }
}
