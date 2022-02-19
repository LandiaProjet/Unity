using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public GameObject EffectKey;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlaying.instance.key = true;
            Instantiate(EffectKey, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
