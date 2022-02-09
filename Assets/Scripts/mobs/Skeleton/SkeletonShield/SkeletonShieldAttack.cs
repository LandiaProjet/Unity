using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonShieldAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public float dommage;

    public void onAttack()
    {
        StartCoroutine(playAnimationAttack(1));
    }

    void Attack()
    {
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, skeletonMovement.AttackRadius, skeletonMovement.collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            isPlaying.instance.addDommage(dommage);
        }
    }

    private IEnumerator playAnimationAttack(int number)
    {
        animator.Play("Skeleton_Shield_attack_" + number);
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.5f);
        if (skeletonMovement.isDie == true)
            yield break;
        animator.Play("Skeleton_Shield_idle");
        skeletonMovement.isAttack = false;
    }
}
