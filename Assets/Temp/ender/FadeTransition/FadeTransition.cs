using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
    public Animator animator;

    public void EnableFadeTransition(float time)
    {
        StartCoroutine(FadeIn(time));
    }

    IEnumerator FadeIn(float time)
    {
        animator.SetBool("FadeIn", true);
        yield return new WaitForSeconds(time);
        animator.SetBool("FadeIn", false);
    }
}
