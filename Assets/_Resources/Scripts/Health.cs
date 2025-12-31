using UnityEngine;

public class Health : MonoBehaviour, IDamagale
{
    [SerializeField] private int health = 100;

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
        DecreaseHealth(damageAmount);
    }
    private void Die()
    {
        Destroy(gameObject);
    }

}
