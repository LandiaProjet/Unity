using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skeletonBossAttack : MonoBehaviour, ISkeletonAttack
{
    public SkeletonMovement skeletonMovement;
    public Animator animator;
    public Rigidbody2D rb;
    public float dommage;
    public float speedDash;

    private int countAttack;
    private int typeAttack;
    private Vector2 direction;
    private bool touchDash;
    private bool stun;

    public void onAttack()
    {
        if (countAttack == 10)
        {
            typeAttack = 2;
            StartCoroutine(playAnimationAttack(2));
            countAttack = 0;
            return;
        }
        typeAttack = 1;
        StartCoroutine(playAnimationAttack(1));
        countAttack++;
    }

    private void FixedUpdate()
    {
        if (skeletonMovement.isAttack && typeAttack == 3 && stun == false && touchDash == false && skeletonMovement.isDie == false)
        {
            Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, skeletonMovement.AttackRadius, skeletonMovement.collisionLayers);

            if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
            {
                touchDash = true;
                Attack();
                rb.velocity = new Vector2(0, 0);
                animator.Play("Skeleton_Boss_idle");
                skeletonMovement.isAttack = false;
            }
            else
            {
                Vector2 direction = this.direction.normalized * speedDash;
                direction.y = rb.velocity.y;
                rb.velocity = direction;
            }
        }
        else if (stun == false && !skeletonMovement.isAttack)
        {
            float distance = Mathf.Abs(PlayerMovement.instance.transform.position.x - transform.position.x);

            if (distance > 10 && Random.Range(1, 100) <= 10)
            {
                StartCoroutine(PlayAnimationDash());
            }
        }
    }

    private IEnumerator PlayAnimationDash()
    {
        Collider2D[] TrackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.radarPoint.position, skeletonMovement.TrackRadius, skeletonMovement.collisionLayers);

        if (TrackCircleResult != null && TrackCircleResult.Length >= 1 && skeletonMovement.isDie == false)
        {
            touchDash = false;
            skeletonMovement.isAttack = true;
            direction = TrackCircleResult[0].transform.position - transform.position;
            animator.Play("Skeleton_Boss_dash");
            typeAttack = 3;
            skeletonMovement.isAttack = true;
            yield return new WaitForSeconds(3f);
            if (touchDash == false)
            {
                animator.Play("Skeleton_Boss_stun");
                stun = true;
                rb.velocity = new Vector2(0, 0);
                yield return new WaitForSeconds(5f);
                animator.Play("Skeleton_Boss_idle");
            }
            skeletonMovement.isAttack = false;
            stun = false;
        }
    }

    bool Attack()
    {
        float dommage = this.dommage;
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(skeletonMovement.AttackPoint.position, skeletonMovement.AttackRadius, skeletonMovement.collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            switch (typeAttack)
            {
                case 2:
                    dommage *= 1.5f;
                    break;
                case 3:
                    dommage *= 2f;
                    break;
            }
            isPlaying.instance.addDommage(dommage);
            Vector4 rotation = PlayerAttack.instance.EulerToQuaternion(new Vector3(0, 0, Random.Range(1, 360)));
            Instantiate(PlayerAttack.instance.Impact, PlayerAttack.instance.transform.position, new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));
            return true;
        }
        return false;
    }

    private IEnumerator playAnimationAttack(int number)
    {
        animator.Play("Skeleton_Boss_attack_" + number);
        skeletonMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.4f);
        if (skeletonMovement.isDie == true)
            yield break;
        animator.Play("Skeleton_Boss_idle");
        skeletonMovement.isAttack = false;
    }
}
