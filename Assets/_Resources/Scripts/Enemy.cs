using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{       

        [SerializeField] private AttackArea attackArea;
        [SerializeField] protected Transform target;
        [SerializeField] protected int damage;
        [SerializeField] protected float distanceToAttack;
        
        protected NavMeshAgent navMeshAgent;
        protected Animator animator;
        protected bool isAttacking;
        private Health health;
        
        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        private void FixedUpdate()
        {
            FollowTarget();
            Attack();
        }

        public void TakeDamage(int damageAmount)
        {
            if (health != null)
            {
                health.TakeDamage(damageAmount);
                Debug.Log(damageAmount);
            }
        }
        
        protected IEnumerator Hit()
        {
            isAttacking = true;
            foreach (var damagables in attackArea.Damagables)
            {
                damagables.TakeDamage(damage);
            }
            yield return new WaitForSeconds(1f); 
            isAttacking = false;
        }

        protected abstract void FollowTarget();
        protected abstract void Attack();

}
