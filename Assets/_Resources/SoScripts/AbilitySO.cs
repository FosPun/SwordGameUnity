using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable Objects/AbilitySO")]

public class AbilitySO : ScriptableObject
{
    public string abilityName;
    public float cooldown;
    public float duration;
}