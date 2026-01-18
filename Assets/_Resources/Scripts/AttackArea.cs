using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{ 
    public int Damage;
    
    [SerializeField] private Collider _collider;


 

    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.GetComponent<IDamagale>();
        if (damagable != null)
        {
            damagable.TakeDamage(Damage);
        }
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
