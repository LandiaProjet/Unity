using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkeletonMageAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public float dommage;
    public float attack1Radius;
    public float attack2Radius;

    private int countAttack = 0;

    public void onAttack()
    {
        float distance = Mathf.Abs(PlayerMovement.instance.transform.position.x - transform.position.x);

        if (distance < attack1Radius)
            StartCoroutine(playAnimationAttack_1());
        else if (distance < attack2Radius)
            StartCoroutine(playAnimationAttack_2());
        else
            StartCoroutine(JoinPlayer());
    }

    void Attack_1()
    {
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, attack1Radius, skeletonMovement.collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            isPlaying.instance.addDommage(dommage);
            Debug.Log("touch");
        }
    }

    void Attack_2()
    {
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, attack2Radius, skeletonMovement.collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            isPlaying.instance.addDommage(dommage);
            Debug.Log("touch");
        }
    }

    private IEnumerator playAnimationAttack_1()
    {
        countAttack++;
        animator.Play("Skeleton_Mage_attack_2");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        if (countAttack > 5)
        {
            StartCoroutine(EscapePlayer());
            countAttack = 0;
        }
        else
        {
            animator.Play("Skeleton_Mage_idle");
            skeletonMovement.isAttack = false;
        }
    }

    private IEnumerator playAnimationAttack_2()
    {
        countAttack++;
        animator.Play("Skeleton_Mage_attack_1");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        if (countAttack > 5)
        {
            StartCoroutine(EscapePlayer());
            countAttack = 0;
        }
        else
        {
            animator.Play("Skeleton_Mage_idle");
            skeletonMovement.isAttack = false;
        }
    }

    private IEnumerator JoinPlayer()
    {
        animator.Play("Skeleton_Mage_teleport");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        transform.position = PlayerMovement.instance.transform.position;
        animator.Play("Skeleton_Mage_teleport_reverse");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        animator.Play("Skeleton_Mage_idle");
        skeletonMovement.isAttack = false;
    }

    private IEnumerator EscapePlayer()
    {
        Debug.Log("Escape");
        animator.Play("Skeleton_Mage_teleport");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        Vector2 newPosition = PlayerMovement.instance.transform.position;
        newPosition.x += (Random.Range(1, 100) <= 50) ? -30 : 30;
        transform.position = newPosition;
        yield return new WaitForSeconds(10f);
        rb.velocity = new Vector2(0, 0);
        transform.position = PlayerMovement.instance.transform.position;
        rb.velocity = new Vector2(0, 0);
        animator.Play("Skeleton_Mage_teleport_reverse");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        animator.Play("Skeleton_Mage_idle");
        skeletonMovement.isAttack = false;
    }
}