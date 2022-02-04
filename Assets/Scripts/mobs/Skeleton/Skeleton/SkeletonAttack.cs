using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;

    public void onAttack()
    {
        StartCoroutine(playAnimationAttack(1));
    }

    private IEnumerator playAnimationAttack(int number)
    {
        animator.Play("Skeleton_attack_" + number);
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);
        animator.Play("Skeleton_idle");
        skeletonMovement.isAttack = false;
    }
}
