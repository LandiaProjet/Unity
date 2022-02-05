using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpearAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public float dommage;

    public void onAttack()
    {
        StartCoroutine(PlayAnimationAttack());
    }

    void Attack()
    {
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, skeletonMovement.AttackRadius, skeletonMovement.collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            isPlaying.instance.addDommage(dommage);
            Debug.Log("touch");
        }
    }

    private IEnumerator PlayAnimationAttack()
    {
        animator.Play("Skeleton_Spear_Attack");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);
        animator.Play("Skeleton_Spear_Idle");
        skeletonMovement.isAttack = false;
    }
}
