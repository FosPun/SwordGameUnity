
using UnityEngine;
using UnityEngine.Events;

public class AttackArea : MonoBehaviour
{
    public UnityEvent OnHit;
    public int Damage;
    
    [SerializeField] private Collider _collider;
    
    private void OnTriggerEnter(Collider other)
    {
        
        var damagable = other.GetComponent<IDamagale>();
        if (damagable != null)
        {
            damagable.TakeDamage(Damage);
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
