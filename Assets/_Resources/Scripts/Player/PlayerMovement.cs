using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnJump;
    public UnityEvent OnLanding;
    
    
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool jumpTrigger;
    
    [HideInInspector] public Collider[] groundColliders;
    
    [Header("Parameters for movement and jump")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;
    
    [Header("Parameters for checking Ground")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    [Header("Model of the Player")]
    [SerializeField] private GameObject model; 
    
    
    private Rigidbody rb;
    
    private InputAction move;
    private InputAction jump;

    private bool canMove = true;
    
    private Vector2 moveValue;

    void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        CheckGround();
        
        if(!canMove) return;
        Jump();
        Rotation();
        Move();
    }

    public void SwitchMovement()
    {
        canMove = !canMove;

    }
    public void StopMovementOnDuration(float timeDelay)
    {
        StartCoroutine(HitStunCoroutine(timeDelay));
    }

    private void CheckGround()
    {
        groundColliders = 
            Physics.OverlapSphere(new Vector3
                (
                    transform.position.x, transform.position.y -  groundCheckDistance,transform.position.z),
                    groundCheckRadius,
                    LayerMask.GetMask("Ground")
                );
        if (groundColliders.Length > 0)
        {
            isGrounded = true;
            OnLanding.Invoke();
        }
        else
        {
            isGrounded = false;
        }
    }

    
    private void GetInput()
    {
        moveValue = move.ReadValue<Vector2>();
        jumpTrigger = jump.IsPressed();
    }
    
    private void Move()
    {
        float targetSpeed = speed * moveValue.x;
        float speedAmount = targetSpeed - rb.linearVelocity.x;
        rb.AddForce( Vector2.right * speedAmount , ForceMode.VelocityChange);
        isMoving = Mathf.Abs(moveValue.x) > 0.01f;

    }
    private void Jump()
    {
        if (!isGrounded || !jumpTrigger) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce( Vector2.up * jumpPower ,ForceMode.Impulse);
        OnJump?.Invoke();
    }
    
    private void Rotation()
    {
        if (Mathf.Abs(moveValue.x) < 0.1f || !canMove) return;
        
        float targetY = moveValue.x > 0 ? 0f : 180f;

        Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);

        rb.MoveRotation(targetRotation);
    }
    
    private IEnumerator HitStunCoroutine(float time)
    {
        rb.linearVelocity = Vector3.zero;
        SwitchMovement();
        yield return new WaitForSeconds(time);
        SwitchMovement();    
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y -  groundCheckDistance,transform.position.z) , groundCheckRadius);
    }
}
