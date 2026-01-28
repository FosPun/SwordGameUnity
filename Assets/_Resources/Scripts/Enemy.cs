using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
        public UnityEvent OnAttack;
        
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public GameObject target;

        public float DistanceForFollowTarget = 5f;
        public float DistanceForLostTarget = 10f;
        public float distanceToAttack = 1f;
        
        public float attackDuration;

        public bool isAttacking { get; private set; }
        
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Collider collider;
        [HideInInspector] public Health targetHealth;
        [HideInInspector] public Animator animator;
        
        private EnemyStateMachine _enemyStateMachine;
        
        
        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            targetHealth = target.GetComponent<Health>();
            collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _enemyStateMachine = new EnemyStateMachine(this);
            _enemyStateMachine.Initialize(_enemyStateMachine.IdleState);

        }

        private void FixedUpdate()
        {
            _enemyStateMachine.Execute();
        }
        
        public void Deactivate()
        {
            _enemyStateMachine.Transition(EnemyStateMachine.DeadState);
        }
        public void Attack()
        {
            if (isAttacking) return;
            StartCoroutine(Hit());
        }
        
        private IEnumerator Hit()
        {
            isAttacking = true;
            OnAttack?.Invoke();
            yield return new WaitForSeconds(attackDuration); 
            isAttacking = false;
        }
        public float CalculateDistanceToTarget()
        {
            return Vector3.Distance(transform.position,target.transform.position);
        }

}
