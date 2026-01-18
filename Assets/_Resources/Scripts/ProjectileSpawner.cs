using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
using UnityEngine.Serialization;

public class ProjectileSpawner : MonoBehaviour
{
    public UnityEvent OnShoot;

    [FormerlySerializedAs("arrowPrefab")]
    [Tooltip("Prefab to shoot")] 
    [SerializeField] private Projectile projectilePrefab;

    [FormerlySerializedAs("arrowVelocity")]
    [Tooltip("Projectile force")]
    [SerializeField] private float prefabVelocity = 1000f;

    [FormerlySerializedAs("arrowSpawnPosition")]
    [Tooltip("End point of gun where arrow appear")]
    [SerializeField] private Transform prefabSpawnPosition;

    
    [SerializeField] private bool collectionCheck = true;


    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    private IObjectPool<Projectile> objectPool;
    private void Awake()
    {
        objectPool = new ObjectPool<Projectile>(CreateObject,OnGetFromPool,OnReleaseToPool,OnDestroyPooledObject, collectionCheck,defaultCapacity,maxSize);
        CreateObject();
    }

    private Projectile CreateObject()
    {
        Projectile projectileInstance = Instantiate(projectilePrefab);
        projectileInstance.ObjectPool = objectPool;
        return projectileInstance;
    }

    private void OnReleaseToPool(Projectile objectToRelease)
    {
        objectToRelease.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Projectile objectToGet)
    {
        objectToGet.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Projectile objectToDestroy)
    {
        Destroy(objectToDestroy);
    }

    public void Shoot()
    {
        Projectile projectileObject = objectPool.Get();
        
        projectileObject.transform.SetPositionAndRotation(prefabSpawnPosition.position, prefabSpawnPosition.rotation);
        
        projectileObject.GetComponent<Rigidbody>().AddForce(projectileObject.transform.forward * prefabVelocity, ForceMode.Acceleration);
        
        projectileObject.Deactivate();
        
        OnShoot?.Invoke();
    }
}