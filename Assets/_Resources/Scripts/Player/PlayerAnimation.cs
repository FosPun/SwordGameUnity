using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }
//TODO: delete LateUpdate and add listeners in Events PlayerMovement and e.t.c
    private void LateUpdate()
    {
        animator.SetBool("isMoving", playerMovement.isMoving);
        animator.SetBool("isGround", playerMovement.isGrounded);
        animator.SetBool("isDead", health.isDead);
    }

    private void SetJump()
    {
        animator.SetTrigger("Jump");
    }

    private void OnEnable()
    {
        playerMovement.OnJump.AddListener(SetJump);
    }

    private void OnDisable()
    {
        playerMovement.OnJump.RemoveListener(SetJump);
    }
}

