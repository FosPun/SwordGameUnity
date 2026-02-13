using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] public Ability[] abilities;
    [SerializeField] private InputAction[] actions;
    

    private void Awake()
    {
        actions = new InputAction[abilities.Length];
        for (int i = 0; i < abilities.Length; i++)
        {
            actions[i] = InputSystem.actions.FindAction(abilities[i].abilitySO.abilityName);
        }
        foreach (Ability ability in abilities)
        {
            ability.Init();
        }
        
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        foreach (Ability ability in abilities)
        {
            ability.ReduceCooldown();
        }
    }

    private void GetInput()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (actions[i].WasPressedThisFrame()) 
            { 
                abilities[i].UseAbility();
            }
        }
    }
}
