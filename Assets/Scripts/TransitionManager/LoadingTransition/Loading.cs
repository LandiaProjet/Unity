using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider slider;

    private void OnEnable()
    {
        StartCoroutine(onLoad());
    }

    IEnumerator onLoad()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.1f);
            slider.value += 1;
        }
    }
}
