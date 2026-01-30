using System.Collections;
using UnityEngine;

public class Dodge : Ability
{

    [SerializeField] private GameObject model;
    
    private Collider collider;
    
    protected override void Init()
    {
        collider = GetComponent<Collider>();
    }
    protected override void UseAbility()
    {
        if (!abilityTrigger || isActive || Timer < cooldownTime) return;
        Timer = 0;
        StartCoroutine(DodgeCoroutine());
    }
    private IEnumerator DodgeCoroutine()
    {
        OnAbilityUse?.Invoke();
        model.SetActive(false);
        isActive = true;
        
        collider.excludeLayers += LayerMask.GetMask("Enemy","Projectile");
        yield return new WaitForSeconds(duration);
        model.SetActive(true);
        isActive = false;
        collider.excludeLayers -= LayerMask.GetMask("Enemy", "Projectile");
    }
}
