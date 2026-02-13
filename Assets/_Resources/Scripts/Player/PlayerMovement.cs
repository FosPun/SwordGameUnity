using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnJump;
    public UnityEvent OnLanding;
    
    
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public Collider[] groundColliders;
    
    [Header("Move parameters")]
    public bool canMove = true;
    [HideInInspector] public bool isMoving;
    [SerializeField] private float speed;
    
    [Header("Jump parameters")]
    [SerializeField] private float jumpPower;
    [SerializeField] private float jumpBufferTime = 0.1f;
    
    private float jumpBufferCounter;
    
    [Header("Parameters for checking Ground")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    
    private Rigidbody rb;
    
    private int groundMask;
    void Awake()
    {
        groundMask = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        CheckGround();
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
                    groundMask
                );
        bool wasGrounded = isGrounded;
        isGrounded = groundColliders.Length > 0;
        if (!wasGrounded && isGrounded)
        {
            OnLanding?.Invoke();
        }
    }

    
    public void SetJumpBuffer(bool value)
    {
        if (value)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    
    public void Move(float value)
    {
        float targetSpeed = speed * value;
        float speedAmount = targetSpeed - rb.linearVelocity.x;
        rb.AddForce( Vector2.right * speedAmount , ForceMode.VelocityChange);
        isMoving = Mathf.Abs(rb.linearVelocity.x) > 0.1f && isGrounded;

    }
    public void Jump()
    {
        if (!isGrounded || jumpBufferCounter <= 0f) return;
        jumpBufferCounter = 0f;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce( Vector2.up * jumpPower ,ForceMode.VelocityChange);
        OnJump?.Invoke();
    }
    
    public void Rotation(float value)
    {
        if (Mathf.Abs(value) < 0.1f || !canMove) return;
        
        float targetY = value > 0 ? 0f : 180f;

        Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);

        rb.MoveRotation(targetRotation);
    }
    
    private IEnumerator HitStunCoroutine(float time)
    {
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
