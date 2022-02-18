using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomIn : MonoBehaviour
{
    public float waitBeforeStart;
    public float valueStart;
    public float Durability;

    private Vector3 FinalScale;

    private void OnEnable()
    {
        FinalScale = transform.localScale;
        transform.localScale = new Vector3(valueStart, valueStart, 1);
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        Vector3 scale = transform.localScale;
        float time = 0;

        yield return new WaitForSeconds(waitBeforeStart);
        while (time <= Durability)
        {
            time = time + Time.deltaTime;
            scale.x = Mathf.Lerp(scale.x, FinalScale.x, time / Durability);
            scale.y = Mathf.Lerp(scale.y, FinalScale.y, time / Durability);
            transform.localScale = scale;
            yield return null;
        }
    }
}
