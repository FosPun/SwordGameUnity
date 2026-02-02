using System.Collections;
using UnityEngine;

public class Dodge : Ability
{

    [SerializeField] private GameObject model;
    
    private Collider _collider;
    private Rigidbody _rigidbody;

    
    protected override void Init()
    {
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
    }
    protected override void UseAbility()
    {
        if(!_playerMovement.isGrounded) return;
        OnAbilityUse?.Invoke();
        Timer = 0;
        StartCoroutine(DodgeCoroutine());
    }
    private IEnumerator DodgeCoroutine()
    {
        
        isActive = true;
        _rigidbody.isKinematic = true;
        _collider.excludeLayers += LayerMask.GetMask("Enemy","Projectile");
        yield return new WaitForSeconds(duration);
        _rigidbody.isKinematic = false;
        isActive = false;
        _collider.excludeLayers -= LayerMask.GetMask("Enemy", "Projectile");
    }
}
