using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        if (Random.Range(1, 100) > 50)
            animator.Play("impact_1");
        else
            animator.Play("impact_2");
    }

    public void DestroyAfter()
    {
        Destroy(gameObject);
    }
}
