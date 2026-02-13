using System;
using System.Collections;
using UnityEngine;
public class Dodge : Ability
{
    [SerializeField] private float distance; 
    [SerializeField] private PlayerMovement _playerMovement;
    private Collider _collider; 
    private Rigidbody _rigidbody;
    public override void Init()
    {
        timer = abilitySO.cooldown;
        CooldownPercentage = 1;
        _collider = _playerMovement.GetComponent<Collider>(); 
        _rigidbody = _playerMovement.GetComponent<Rigidbody>(); 
    }
    public override void UseAbility() 
    { 
        if(!_playerMovement.isGrounded || CooldownPercentage < 1) return; 
        OnAbilityUse?.Invoke(); 
        timer = 0; 
        _playerMovement.StartCoroutine(DodgeCoroutine()); 
    }

    private IEnumerator DodgeCoroutine()
    {
        _playerMovement.StopMovementOnDuration(abilitySO.duration);
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.AddForce(distance * _playerMovement.transform.right, ForceMode.VelocityChange);
        _collider.excludeLayers += LayerMask.GetMask("Enemy","Projectile");
        yield return new WaitForSeconds(abilitySO.duration);
        _collider.excludeLayers -= LayerMask.GetMask("Enemy", "Projectile");
    }
}
