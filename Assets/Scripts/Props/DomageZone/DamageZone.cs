using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public float damage;

    private bool isExecuting = false;
    private bool isStaying = false;


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isStaying = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isStaying = true;
            StartCoroutine(StartDamage());
        }
    }

    IEnumerator StartDamage()
    {
        if (!isExecuting)
        {
            isExecuting = true;
            while (isStaying)
            {
                isPlaying.instance.addDommage(damage);
                yield return new WaitForSeconds(1f);
            }
            isExecuting = false;
        }
    }
}