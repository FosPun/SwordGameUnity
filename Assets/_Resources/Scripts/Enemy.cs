using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
        private EnemyStateMachine _enemyStateMachine;
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

        public UnityEvent OnAttack;
        public GameObject target;

        public float DistanceForFollowTarget = 5f;
        public float DistanceForLostTarget = 10f;
        public float distanceToAttack = 1f;
        
        [SerializeField] private int damage;
        [SerializeField] private int attackCooldown;
        
        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Collider collider;
        [HideInInspector] public Health health;
        [HideInInspector] public Health targetHealth;
        [HideInInspector] public Animator animator;
        
        private AttackArea attackArea;
        
        protected bool isAttacking;
        
        private void Awake()
        {
            target = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            attackArea = GetComponent<AttackArea>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
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
            OnAttack?.Invoke();
            StartCoroutine(Hit());
        }
        
        private IEnumerator Hit()
        {
            isAttacking = true;
            yield return new WaitForSeconds(attackCooldown); 
            isAttacking = false;
        }
        public float CalculateDistanceToTarget()
        {
            return Vector3.Distance(transform.position,target.transform.position);
        }

}
