using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordAttack : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;

    void Update()
    {
        if (playerMovement.isGrounded && !playerMovement.isAttack)
        {
            if (Input.GetKeyDown(KeyCode.H))
                StartCoroutine(playAnimationAttack(1));
            if (Input.GetKeyDown(KeyCode.B))
                StartCoroutine(playAnimationAttack(2));
            if (Input.GetKeyDown(KeyCode.C))
                StartCoroutine(playAnimationAttack(3));
        }
    }

    private IEnumerator playAnimationAttack(int number)
    {
        animator.Play("Player_attack_" + number);
        playerMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.Play("Player_idle");
        playerMovement.isAttack = false;
    }
}
