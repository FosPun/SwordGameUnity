using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnDamaged;
    public UnityEvent OnDie;
    
    public bool isDead { get ; private set ; }
    [SerializeField] private int health = 100;
    [Tooltip("Time for die animation.")]
    
    [SerializeField] private GameObject mesh;
    [SerializeField] private ParticleSystem deathParticles;
    
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
        if (mesh != null && deathParticles != null)
        {
            StartCoroutine(DieAnim());
        }
        OnDie?.Invoke();
    }

    private IEnumerator DieAnim()
    {
        yield return new WaitForSeconds(5);
        mesh.SetActive(false);
        deathParticles.Play();
        Destroy(gameObject,1f);
    }
}
