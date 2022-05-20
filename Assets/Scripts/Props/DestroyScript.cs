using UnityEngine;
using System.Collections;

public class DestroyScript : Enemy
{
    public Animator animator;
    public Collider2D collision;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        collision = transform.GetComponent<Collider2D>();
    }

    public void DestroyBarrel()
    {
        StartCoroutine(onDestroyBarrel());
    }

    private IEnumerator onDestroyBarrel()
    {
        collision.enabled = false;
        animator.SetTrigger("Destroy");
        SoundManager.instance.PlayEffectSound(2);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public override void onDie()
    {
        CoinSpawner.instance.SpawnCoins(Random.Range(1, 5), transform);
        StartCoroutine(onDestroyBarrel());
    }
}
