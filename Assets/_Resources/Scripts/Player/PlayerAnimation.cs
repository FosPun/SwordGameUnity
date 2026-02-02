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

    private void Update()
    {
        animator.SetBool("isMoving", playerMovement.isMoving);
        animator.SetBool("isGround", playerMovement.isGrounded);
        animator.SetBool("isDead", health.isDead);
        if (playerMovement.jumpTrigger && playerMovement.isGrounded) animator.SetTrigger("Jump");
    }
}
