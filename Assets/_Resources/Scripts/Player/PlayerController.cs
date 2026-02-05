using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Components
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;

    //Input Actions
    private InputAction move;
    private InputAction jump;
    private InputAction attack;

    private bool attackTrigger;
    
    private Vector2 moveValue;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAttack = GetComponent<PlayerAttack>();
        
        attack = InputSystem.actions.FindAction("Attack");
        move = InputSystem.actions.FindAction("Move");
        jump = InputSystem.actions.FindAction("Jump");
    }

    private void Update()
    {
        ReadInput();
        Attack();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Attack()
    {
        if (!attackTrigger) return;
        
        _playerAttack.Attack();
        
    }

    private void Movement()
    {
        
        if(!_playerMovement.canMove) return;
        _playerMovement.Move(moveValue.x);
        _playerMovement.Rotation(moveValue.x);
        _playerMovement.Jump();
    }

    private void ReadInput()
    {  
        moveValue = move.ReadValue<Vector2>();
        _playerMovement.SetJumpBuffer(jump.WasPressedThisFrame());
        attackTrigger = attack.IsPressed();
        
    }
}
