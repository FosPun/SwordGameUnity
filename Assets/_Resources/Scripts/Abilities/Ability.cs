using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    public UnityEvent OnAbilityUse;

    public float CooldownPercentage;
    
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected float duration;
    
    protected PlayerMovement _playerMovement;
    
    protected float Timer;
    
    protected bool isActive;
   
    private bool abilityTrigger;
    
    
    [SerializeField] private string abilityName;
    
    private InputAction abilityAction;
    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
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
        if(!abilityTrigger || isActive || Timer < cooldownTime) return;
        UseAbility();
    }

    private void ReduceCooldown()
    {
        if (Timer > cooldownTime + Time.fixedDeltaTime) return;
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
