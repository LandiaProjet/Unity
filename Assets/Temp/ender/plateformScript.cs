using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformScript : MonoBehaviour
{
    public Collider2D collider2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (transform.position.y < collision.transform.position.y)
            {
                collider2D.isTrigger = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider2D.isTrigger = false;
    }
}
