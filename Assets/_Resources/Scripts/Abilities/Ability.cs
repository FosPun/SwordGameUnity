using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    public UnityEvent OnAbilityUse;

    public float CooldownPercentage;
    
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float duration;
    
    protected PlayerMovement playerMovement;
    
    protected float Timer;
    
    protected bool isActive;
    protected bool abilityTrigger;
    
    
    [SerializeField] private string abilityName;
    
    private InputAction abilityAction;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        abilityAction = InputSystem.actions.FindAction(abilityName);
        Timer = cooldownTime;
        CooldownPercentage = 1;
        Init();
    }

    private void Update()
    {
        GetInput();   
    }

    private void FixedUpdate()
    {
        ReduceCooldown();
        UseAbility();
    }

    private void ReduceCooldown()
    {
        if (Timer > cooldownTime) return;
        CooldownPercentage = Timer / cooldownTime;
        Timer += Time.deltaTime;
    }
    private void GetInput()
    {
        abilityTrigger = abilityAction.IsPressed();
    }
    protected abstract void UseAbility();
    protected abstract void Init();
    
    
}
