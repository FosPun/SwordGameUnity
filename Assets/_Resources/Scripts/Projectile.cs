using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject particlesOnHit;
    [SerializeField] private float timeoutDelay = 3f;
    
    private IObjectPool<Projectile> objectPool;

    private bool hit = false;
    // Public property to give the projectile a reference to its ObjectPool
    public IObjectPool<Projectile> ObjectPool
    {
        set => objectPool = value;
    }

    
    private void OnEnable()
    {
        StopAllCoroutines();
        Deactivate();
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hit) return;
        hit = true;
        if (particlesOnHit != null)
        {
            Instantiate(particlesOnHit, transform.position, Quaternion.identity);
        }
        StartCoroutine(DeactivateRoutine(0f));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        hit = false;

        // Release the projectile back to the pool
        objectPool.Release(this);
    }
}
