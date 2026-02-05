using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
        public UnityEvent OnAttack;
        public EnemySO enemySO;
        public GameObject Target;
        public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;
        public bool isAttacking { get; private set; }

        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Collider collider;
        [HideInInspector] public Health targetHealth;
        [HideInInspector] public Animator animator;
        
        private EnemyStateMachine _enemyStateMachine;


        private void Awake()
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            targetHealth = Target.GetComponent<Health>();
            collider = GetComponent<Collider>();
            navMeshAgent.stoppingDistance = enemySO.DistanceToAttack;
        }

        private void Start()
        {
            _enemyStateMachine = new EnemyStateMachine(this);
            _enemyStateMachine.Initialize(_enemyStateMachine.IdleState);

        }

        private void Update()
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
            yield return new WaitForSeconds(enemySO.attackDuration); 
            isAttacking = false;
        }
        public float CalculateDistanceToTarget()
        {
            return Vector3.Distance(transform.position,Target.transform.position);
        }

}
