using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkeletonAttack
{
    public void onAttack();
}

public class SkeletonMovement : MonoBehaviour
{
    public MonoBehaviour AttackScript;
    private ISkeletonAttack skeletonAttack;

    public Animator animator;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;

    public Transform radarPoint;
    public float TrackRadius;
    public Transform AttackPoint;
    public float AttackRadius;
    public LayerMask collisionLayers;

    public bool isDie;
    public bool isAttack;
    public float speed;

    private void Start()
    {
        skeletonAttack = (ISkeletonAttack)AttackScript;
    }

    private void Update()
    {
        float characterVelocityX = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocityX);
    }

    void FixedUpdate()
    {

        if (!isAttack)
        {
            Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, collisionLayers);
            Collider2D[] TrackCircleResult = Physics2D.OverlapCircleAll(radarPoint.position, TrackRadius, collisionLayers);

            Flip(rb.velocity.x);
            if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
            {
                skeletonAttack.onAttack();
            } else if (TrackCircleResult != null && TrackCircleResult.Length >= 1) {
                Vector2 direction = (TrackCircleResult[0].transform.position - transform.position).normalized * speed;
                direction.y = rb.velocity.y;
                rb.velocity = direction;
            }
        }
    }

    void Flip(float _velocityX)
    {
        if (_velocityX > 0.1f)
        {
            sprite.flipX = false;
        }
        else if (_velocityX < -0.1f)
        {
            sprite.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(radarPoint.position, TrackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }
}
