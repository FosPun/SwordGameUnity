using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnJump;
    public UnityEvent OnDodge;
    
    
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isDodging;
    [HideInInspector] public bool jumpTrigger;
    [HideInInspector] public float dodgeCooldownTime;
    
    [Header("Parameters for movement and jump")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float rotationSpeed;
    [Header("Parameters for Dodge")]
    [SerializeField] private float dodgeDuration;
    [SerializeField] private float dodgeDistance;
    [SerializeField] private float dodgeCooldown = 5f;
    [Header("Parameters for checking Ground")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float groundCheckRadius = 0.2f;
    
    [Header("Model of the Player")]
    [SerializeField] private GameObject model;
    
    
    private Rigidbody rb;
    private Collider collider;
    
    private InputAction move;
    private InputAction jump;
    private InputAction crouch;

    private bool canMove = true;
    private bool crouchKeyDown;
    
    private Vector2 moveValue;
    
    private Collider[] groundColliders = new Collider[10];

    void Awake()
    {
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
        crouch = InputSystem.actions.FindAction("Crouch");
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        dodgeCooldownTime = dodgeCooldown;
    }

    void Update()
    {
        GetInput();
       
    }

    private void FixedUpdate()
    {
        CheckGround();
        Jump();
        Rotation();
        Movement();
        Dodge();
        DodgeCooldown();
    }


    private void CheckGround()
    {
        groundColliders = Physics.OverlapSphere( new Vector3(transform.position.x, transform.position.y -  groundCheckDistance,transform.position.z) , groundCheckRadius, LayerMask.GetMask("Ground"));
        isGrounded = groundColliders.Length > 0;
    }

    private void GetInput()
    {
        moveValue = move.ReadValue<Vector2>();
        jumpTrigger = jump.IsPressed();
        crouchKeyDown = crouch.IsPressed();
    }
    
    private void Movement()
    {
        if(!canMove || !isGrounded) return;
        rb.linearVelocity = new Vector2(moveValue.x * speed, rb.linearVelocity.y);
        isMoving = moveValue.x != 0 ? true : false;
    }
    private void Jump()
    {
        if (!jumpTrigger || !isGrounded) return;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x,jumpPower);
        OnJump?.Invoke();
    }   
    private void Rotation()
    {
        if (Mathf.Abs(moveValue.x) < 0.1f) return;
        
        float targetY = moveValue.x > 0 ? 0f : 180f;

        Quaternion targetRotation = Quaternion.Euler(0f, targetY, 0f);

        rb.MoveRotation(targetRotation);
    }
    private void DodgeCooldown()
    {
        if(dodgeCooldownTime >= dodgeCooldown) return;
        dodgeCooldownTime += Time.fixedDeltaTime;
    }

    private void Dodge()
    {
        if(!crouchKeyDown || isDodging || dodgeCooldownTime < dodgeCooldown) return;
        dodgeCooldownTime = 0;
        StartCoroutine(DodgeCoroutine());
    }
    
    public void FullStop()
    {
        rb.linearVelocity = Vector3.zero;
        StopAllCoroutines();
        canMove = false;
    }
    public void HitStun(float timeDelay)
    {
        StartCoroutine(HitStunCoroutine(timeDelay));
    }
    private IEnumerator DodgeCoroutine()
    {
        OnDodge?.Invoke();
        model.SetActive(false);
        isDodging = true;
        canMove = false;
        
        collider.excludeLayers += LayerMask.GetMask("Enemy","Projectile");
        
        rb.linearVelocity =  Vector3.zero;
        rb.linearVelocity = new Vector2(transform.right.x * dodgeDistance, rb.linearVelocity.y);
        
        yield return new WaitForSeconds(dodgeDuration);
        model.SetActive(true);
        canMove = true;
        isDodging = false;
        collider.excludeLayers -= LayerMask.GetMask("Enemy", "Projectile");
    }
    private IEnumerator HitStunCoroutine(float time)
    {
        rb.linearVelocity = Vector3.zero;
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y -  groundCheckDistance,transform.position.z) , groundCheckRadius);
    }
}
