using UnityEngine;

namespace _Resources.Scripts
{
    
    public class TrapTrigger : TriggerEvent
    {
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>())
            {
                OnEnter?.Invoke();
            }
        }
    }
}