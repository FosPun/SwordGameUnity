using UnityEngine;

public class SimpleEnemy : Enemy
{
    protected override void FollowTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }

    protected override void Attack()
    {
        while (Vector3.Distance(transform.position, target.position) < distanceToAttack)
        {
            if(isAttacking) return;
            StartCoroutine(Hit());
        }
    }
}
