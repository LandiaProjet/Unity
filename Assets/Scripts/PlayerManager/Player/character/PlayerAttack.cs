using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Arrow;
    public Transform AttackPoint;
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
        }
    }

    public void Attack() {
        if (playerMovement.isGrounded && !playerMovement.isAttack)
        {
            if (PlayerManager.instance.mode == "sword") {
                StartCoroutine(playAnimationAttack(1));
            } else if (PlayerManager.instance.mode == "bow") {
                StartCoroutine(playAnimationShoot());
            }
        }
        
    }

    /**
     * Bow Attack
     */
    private IEnumerator playAnimationShoot()
    {
        if (HudManager.instance.getArrow() > 0)
        {
            animator.Play("Player_attack1");
            playerMovement.isAttack = true;
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(1.2f);
            animator.Play("Player_idle");
            playerMovement.isAttack = false;
        }
    }

    public void shoot()
    {
        if (HudManager.instance.getArrow() <= 0)
            return;
        if (transform.localScale.x == -1)
            AttackPoint.Rotate(0, 0, 180);
        GameObject arrow = Instantiate(Arrow, AttackPoint.position, AttackPoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f, onHit);
        isPlaying.instance.RemoveArrow();
    }

    void onHit()
    {
        Debug.Log("c'est bon");
    }

    /**
     * Sword Attack
     */
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
