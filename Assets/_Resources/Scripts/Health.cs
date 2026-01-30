using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamagale
{
    public UnityEvent OnDamaged;
    public UnityEvent OnDie;
    
    public bool isDead { get ; private set ; }
    [SerializeField] private int health = 100;
    [Tooltip("Time for die animation.")]
    [SerializeField] private float dieDuration = 2f;
    
    private bool canDamage = true;
    
    private void DecreaseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (health <= 0 || !canDamage) return;
        OnDamaged?.Invoke();
        DecreaseHealth(damageAmount);
    }
    
    private void Die()
    {
        isDead = true;
        OnDie?.Invoke();
    }
}
