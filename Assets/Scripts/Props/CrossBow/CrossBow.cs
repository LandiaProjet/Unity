using UnityEngine;
using System.Collections;

public class CrossBow : Enemy
{
    public Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public SpriteRenderer sprite;

    public float timeShoot = 3f;

    private Collider2D collision;

    void PlayAnimation()
    {
        animator.SetBool("Shoot", true);
    }

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        collision = transform.GetComponent<Collider2D>();
        if(!sprite.flipX)
            firePoint.Rotate(180,0,180);
        
        InvokeRepeating("PlayAnimation", 1f, timeShoot);
    }

    private IEnumerator Destroy()
    {
        collision.enabled = false;
        animator.SetTrigger("Destroy");
        SoundManager.instance.PlayEffectSound(2);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        animator.SetBool("Shoot", false);
        GameObject arrow = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f);
    }
    public override void onDie()
    {
        CoinSpawner.instance.SpawnCoins(Random.Range(1, 5), transform);
        StartCoroutine(Destroy());
    }
}