using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class CrossBow : MonoBehaviour
{
    public Animator animator;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public SpriteRenderer sprite;
    
    private void Start()
    {
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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void Shoot()
    {
        GameObject arrow = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        arrowScript.launchArrow(50f, onHit);
    }

    void onHit()
    {
        Debug.Log("c'est bon");
    }

    private IEnumerator DeleteAfterShoot(GameObject arrowObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(arrowObject);
    }
    
}