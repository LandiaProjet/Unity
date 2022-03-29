using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Arrow;
    public GameObject Impact;
    public Transform AttackPoint;
    public float AttackRadius;
    public SpriteRenderer sprite;
    public Animator animator;
    public Rigidbody2D rb;
    public PlayerMovement playerMovement;
    public LayerMask collisionLayers;

    public static PlayerAttack instance;
    public void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerAttack dans la scene");
            return;
        }
        instance = this;
    }

    void Update()
    {
        if (playerMovement.isGrounded && !playerMovement.isAttack)
        {
            if (Input.GetKeyDown(KeyCode.H))
                StartCoroutine(playAnimationAttack(1));
        }
    }

    public void Attack() {
        if (playerMovement.isGrounded && !playerMovement.isAttack)
        {
            if (PlayerManager.instance.mode == "sword") {
                StartCoroutine(playAnimationAttack(1));
            } else if (PlayerManager.instance.mode == "bow") {
                StartCoroutine(playAnimationShoot());
            }
        }
        
    }

    /**
     * Bow Attack
     */
    private IEnumerator playAnimationShoot()
    {
        if (HudManager.instance.getArrow() > 0)
        {
            animator.Play("Player_attack1");
            playerMovement.isAttack = true;
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(1.2f);
            animator.Play("Player_idle");
            playerMovement.isAttack = false;
        }
    }

    public void shoot()
    {
        if (HudManager.instance.getArrow() <= 0)
            return;
        if (transform.localScale.x == -1)
            AttackPoint.Rotate(0, 0, 180);
        GameObject arrow = Instantiate(Arrow, AttackPoint.position, AttackPoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f);
        arrowScript.isPlayer = true;
        isPlaying.instance.RemoveArrow();
    }

    void onHit(float damage)
    {
        Collider2D[] AttackCircleResult = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRadius, collisionLayers);

        if (AttackCircleResult != null && AttackCircleResult.Length >= 1)
        {
            Enemy enemy = AttackCircleResult[0].GetComponent<Enemy>();
            enemy.ReceiveDommage(damage);
            Vector4 rotation = EulerToQuaternion(new Vector3(0, 0, Random.Range(1, 360)));
            Instantiate(Impact, enemy.transform.position, new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w));
            Debug.Log("touchSkeleton");
        }
    }

    public Vector4 EulerToQuaternion(Vector3 p)
    {
        p.x *= Mathf.Deg2Rad;
        p.y *= Mathf.Deg2Rad;
        p.z *= Mathf.Deg2Rad;
        Vector4 q;
        float cy = Mathf.Cos(p.z * 0.5f);
        float sy = Mathf.Sin(p.z * 0.5f);
        float cr = Mathf.Cos(p.y * 0.5f);
        float sr = Mathf.Sin(p.y * 0.5f);
        float cp = Mathf.Cos(p.x * 0.5f);
        float sp = Mathf.Sin(p.x * 0.5f);
        q.w = cy * cr * cp + sy * sr * sp;
        q.x = cy * cr * sp + sy * sr * cp;
        q.y = cy * sr * cp - sy * cr * sp;
        q.z = sy * cr * cp - cy * sr * sp;
        return q;
    }

    /**
     * Sword Attack
     */
    private IEnumerator playAnimationAttack(int number)
    {
        animator.Play("Player_attack_" + number);
        playerMovement.isAttack = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.Play("Player_idle");
        playerMovement.isAttack = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRadius);
    }
}
