
using UnityEngine;
using UnityEngine.Events;

public class AttackArea : MonoBehaviour
{
    public UnityEvent OnHit;
    
    [SerializeField] private Collider _collider;

    [SerializeField] private int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other);
    }

    public void SetDamage(int damageAmount)
    {
        this.damage = damageAmount;
    }
    private void ApplyDamage(Collider other)
    {
        var damagable = other.GetComponent<IDamageable>();
        if (damagable != null)
        {
            damagable.TakeDamage(damage);
        }
        OnHit?.Invoke();
    }


    public void ActivateCollider()
    {
        _collider.enabled = true;
    }

    public void DeactivateCollider()
    {
        _collider.enabled = false;
    }
}
