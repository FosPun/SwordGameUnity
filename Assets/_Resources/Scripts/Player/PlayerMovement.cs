using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnJump;
    
    
    [HideInInspector] public bool isMoving;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;
    
    private Rigidbody rb;
    
    private InputAction move;
    private InputAction jump;

    private bool jumpTrigger;
    private bool isGrounded;
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
        Jump();
        Rotation();
        Movement();
    }

    private void GetInput()
    {
        moveValue = move.ReadValue<Vector2>();
        jumpTrigger = jump.IsPressed();
    }
    private void Movement()
    {
        rb.linearVelocity = new Vector2(moveValue.x * speed, rb.linearVelocity.y);
        isMoving = moveValue.x != 0 ? true : false;
    }
    private void Jump()
    {
        if (!jumpTrigger || !isGrounded ) return;
        OnJump?.Invoke();
        isGrounded = false;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpPower);
    }   
    //TODO: Smooth Rotation
    private void Rotation()
    {
        if (Mathf.Abs(moveValue.x) < 0.1f) return;
        
        float targetY = moveValue.x > 0 ? 0f : 180f;

        Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);

        rb.MoveRotation(targetRotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
    }
}
