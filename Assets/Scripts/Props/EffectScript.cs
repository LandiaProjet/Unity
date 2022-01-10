using System.Collections;
using UnityEngine;

public class EffectScript : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DeleteAfterAnimation());
    }

    private IEnumerator DeleteAfterAnimation()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
