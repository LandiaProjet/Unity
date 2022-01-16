using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Arrow;
    public Transform ArrowSpawn;
    public SpriteRenderer sprite;
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;

    void Start()
    {
        if (!sprite.flipX)
            ArrowSpawn.Rotate(180, 0, 180);
    }

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
        animator.Play("Player_attack1");
        playerMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1.2f);
        animator.Play("Player_idle");
        playerMovement.isAttack = false;
    }

    public void shoot()
    {
        var arrow = Instantiate(Arrow, ArrowSpawn.position, ArrowSpawn.rotation);
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
