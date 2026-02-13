using UnityEngine;
using UnityEngine.Events;

public abstract class Ability : MonoBehaviour
{
    public UnityEvent OnAbilityUse;
    public AbilitySO abilitySO;
    
    public float CooldownPercentage;

    protected float timer;

    public virtual void ReduceCooldown()
    {
        if (timer > abilitySO.cooldown + Time.fixedDeltaTime) return;
        CooldownPercentage = timer / abilitySO.cooldown;
        timer += Time.deltaTime;
    }
    public abstract void Init();
    public abstract void UseAbility();
}
