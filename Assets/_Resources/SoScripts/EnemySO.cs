using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "EnemySO", menuName = "Scriptable Objects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public float Damage = 1f;
    public float DistanceForFollowTarget = 5f;
    public float DistanceForLostTarget = 10f;
    public float DistanceToAttack = 1f;
        
    public float attackDuration;


}
