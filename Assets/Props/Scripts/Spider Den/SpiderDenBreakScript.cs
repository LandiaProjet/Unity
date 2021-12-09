using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDenBreakScript : MonoBehaviour
{
    public bool isDestroyed = false;
    public Animator animator;
    public Collider2D collision;

    void Start()
    {
        animator = transform.GetComponent<Animator>();
        collision = transform.GetComponent<Collider2D>();
    }

    void Update()
    {
        // Condition à supprimer c'est pour les tests
        if (Input.GetKeyDown(KeyCode.T))
        {
            Break();
        }
    }

    public void Break()
    {
        StartCoroutine(onBreak());
    }

    private IEnumerator onBreak()
    {
        animator.SetTrigger("Break");
        yield return new WaitForSeconds(0.3f);
        collision.enabled = false;
        isDestroyed = true;
    }
}
