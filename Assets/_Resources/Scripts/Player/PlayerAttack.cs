using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    public UnityEvent OnAttack;
    
    [SerializeField] private int damage;
    
    [SerializeField] private float attackCooldown = 1f;
    
    [SerializeField] private AttackArea attackArea;
    
    private Health _health;
    
    private bool isAttacking;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void Attack()
    {   
        if(isAttacking || _health.isDead) return;
        OnAttack?.Invoke();
        StartCoroutine(Hit());
    }
//ToDo Add timings, cooldowns, animations 
    private IEnumerator Hit()
    {
        isAttacking = true;
        attackArea.SetDamage(damage);
        yield return new WaitForSeconds(attackCooldown); 
        isAttacking = false;
    }
}
