using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    public UnityEvent OnCollision;
    [SerializeField] int trapDamage = 1;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Health>())
        {
            other.gameObject.GetComponent<Health>().TakeDamage(trapDamage);
            OnCollision.Invoke();
        }
    }
}
