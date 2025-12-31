using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public List<IDamagale> Damagables { get; } = new();
    
    private void OnTriggerEnter(Collider other)
    {
        var damagable = other.GetComponent<IDamagale>();
        if (damagable != null)
        {
            Damagables.Add(damagable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var damagable = other.GetComponent<IDamagale>();
        if (damagable != null && Damagables.Contains(damagable))
        {
            Damagables.Remove(damagable);
        }
    }
}
