using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private GameObject otherGameObject;
    [SerializeField] private float timeToDestroy;

    private void Awake()
    {
        Destroy(otherGameObject, timeToDestroy);
    }
}
