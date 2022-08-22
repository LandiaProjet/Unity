using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformScript : MonoBehaviour
{
    public Collider2D co;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                co.isTrigger = false;
            }
            else
            {
                co.isTrigger = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            co.isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            co.isTrigger = true;
        }
    }
}
