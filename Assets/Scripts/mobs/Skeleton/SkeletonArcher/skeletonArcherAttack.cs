using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonArcherAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public GameObject arrowPrefabs;

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
        arrowScript.launchArrow(50f, onHit);
    }

    void onHit()
    {
        Debug.Log("c'est bon");
    }

    private IEnumerator PlayAnimationAttack()
    {
        animator.Play("Skeleton_Archer_attack");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1f);
        animator.Play("Skeleton_Archer_idle");
        skeletonMovement.isAttack = false;
    }
}
