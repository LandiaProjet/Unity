using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBowAttack : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (playerMovement.isGrounded && !playerMovement.isAttack)
                StartCoroutine(playAnimationShoot());
        }
    }

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
}
