using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public BoxCollider2D box;

    private float angle;

    public void launchArrow(float force, UnityAction call)
    {
        rb.velocity = transform.right * force;
        call();
    }

    private void Update()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
            StartCoroutine(StopColision());

    }

    IEnumerator StopColision()
    {
        yield return new WaitForSeconds(0.04f);
        rb.simulated = false;
        yield return new WaitForSeconds(25f);
        Destroy(gameObject);
    }

}