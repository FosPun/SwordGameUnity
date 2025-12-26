using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;
    
    //Components
    private Rigidbody rb;
    
    private InputAction move;
    private InputAction jump;

    private Vector2 moveValue;
    private bool jumpTrigger;
    private bool isGrounded;
    void Start()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInputValue();
        
        if (jump.IsPressed()) { jumpTrigger = true;}
    }

    private void FixedUpdate()
    {
        Jump();
        Rotation();
        Movement();
    }

    private void GetInputValue()
    {
        moveValue = move.ReadValue<Vector2>();
        jumpTrigger = jump.IsPressed();
    }
    private void Movement()
    {
        rb.linearVelocity = new Vector2(moveValue.x * speed, rb.linearVelocity.y);
        
    }

    private void Jump()
    {
        if (!jumpTrigger || !isGrounded ) return;
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
