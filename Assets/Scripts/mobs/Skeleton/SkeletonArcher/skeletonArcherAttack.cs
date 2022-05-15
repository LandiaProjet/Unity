using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonArcherAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject arrowPrefabs;


    public int GiveCountCoins()
    {
        return Random.Range(3, 8);
    }

    public void onAttack()
    {
        StartCoroutine(PlayAnimationAttack());
    }

    void Shoot()
    {
        Quaternion rotation = new Quaternion();

        rotation.z = (skeletonMovement.transform.localScale.x > 0) ? 0 : 180;
        GameObject arrow = Instantiate(arrowPrefabs, skeletonMovement.AttackPoint.position, rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f);
    }

    private IEnumerator PlayAnimationAttack()
    {
        animator.Play("Skeleton_Archer_attack");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1f);
        if (skeletonMovement.isDie == true)
            yield break;
        animator.Play("Skeleton_Archer_idle");
        yield return new WaitForSeconds(1.0f);
        skeletonMovement.isAttack = false;
    }
}
