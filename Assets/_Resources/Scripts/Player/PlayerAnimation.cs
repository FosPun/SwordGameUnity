using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        animator.SetBool("isMoving", playerMovement.isMoving);
        animator.SetBool("isGround", playerMovement.isGrounded);
        if (playerMovement.jumpTrigger && playerMovement.isGrounded) animator.SetTrigger("Jump");
    }
}
