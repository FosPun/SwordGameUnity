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
        if (health <= 0) return;
        OnDamaged?.Invoke();
        DecreaseHealth(damageAmount);
    }

    public void Die()
    {
        OnDie?.Invoke();
        isDead = true;
    }
}
