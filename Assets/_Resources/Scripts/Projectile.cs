using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float timeoutDelay = 3f;

    private IObjectPool<Projectile> objectPool;

    // Public property to give the projectile a reference to its ObjectPool
    public IObjectPool<Projectile> ObjectPool
    {
        set => objectPool = value;
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(timeoutDelay));
    }

    private void OnTriggerEnter(Collider other)
    {
        objectPool.Release(this);
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Reset the moving Rigidbody
        Rigidbody rBody = GetComponent<Rigidbody>();
        rBody.linearVelocity = new Vector3(0f, 0f, 0f);
        rBody.angularVelocity = new Vector3(0f, 0f, 0f);

        // Release the projectile back to the pool
        objectPool.Release(this);
    }
}
