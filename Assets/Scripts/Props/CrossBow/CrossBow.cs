using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class CrossBow : Enemy
{
    public Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public SpriteRenderer sprite;

    public float life;
    
    private void Start()
    {
        this.health = life;
        animator = transform.GetComponent<Animator>();
        if(!sprite.flipX)
            firePoint.Rotate(180,0,180);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        animator.SetTrigger("Destroy");
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f);
    }

    private IEnumerator DeleteAfterShoot(GameObject arrowObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(arrowObject);
    }

    public override void onDie()
    {
        StartCoroutine(Destroy());
    }
}