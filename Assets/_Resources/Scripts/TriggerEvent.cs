using UnityEngine;
using UnityEngine.Events;

namespace _Resources.Scripts
{
    public abstract class TriggerEvent : MonoBehaviour
    {
        public UnityEvent OnEnter;

        protected abstract void OnTriggerEnter(Collider other);


    }
}