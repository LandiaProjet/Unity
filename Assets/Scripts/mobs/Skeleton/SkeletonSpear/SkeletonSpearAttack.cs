using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSpearAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public float dommage;

    public int GiveCountCoins()
    {
        return Random.Range(3, 7);
    }

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
            Vector4 rotation = PlayerAttack.instance.EulerToQuaternion(new Vector3(0, 0, Random.Range(1, 360)));
            Instantiate(PlayerAttack.instance.Impact, PlayerAttack.instance.transform.position, new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));
        }
    }

    private IEnumerator PlayAnimationAttack()
    {
        animator.Play("Skeleton_Spear_Attack");
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.2f);
        if (skeletonMovement.isDie == true)
            yield break;
        animator.Play("Skeleton_Spear_Idle");
        skeletonMovement.isAttack = false;
    }
}
