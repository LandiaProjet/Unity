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

    private bool _isShoot;
    private void Start()
    {
        animator = transform.GetComponent<Animator>();
        if(!sprite.flipX)
            firePoint.Rotate(180,0,180);
    }
    void Update()
    {
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        float currentTime = animState.normalizedTime % 1;
        if (Math.Abs(currentTime - 0.80) < 0.01)
        {
            Shoot();
        }
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

    private void Shoot()
    {
        if (_isShoot) return;
        var arrow = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        _isShoot = true;
        StartCoroutine(DeleteAfterShoot(arrow));
    }
    
    private IEnumerator DeleteAfterShoot(GameObject arrowObject)
    {
        yield return new WaitForSeconds(1f);
        Destroy(arrowObject);
        _isShoot = false;
    }
    
}