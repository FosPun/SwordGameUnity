using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        animator.SetBool("isMoving", playerMovement.isMoving);
        
    }
}
