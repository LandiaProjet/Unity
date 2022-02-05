using System;
using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public BoxCollider2D box;

    public void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(StopColision());
    }

    IEnumerator StopColision()
    {
        yield return new WaitForSeconds(0.5f);
        rb.simulated = false;
        yield return new WaitForSeconds(25f);
        Destroy(gameObject);
    }

}