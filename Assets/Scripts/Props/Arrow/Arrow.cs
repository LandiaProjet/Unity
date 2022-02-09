using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public BoxCollider2D box;
    public float damage;

    private float angle;
    private bool used;

    public void launchArrow(float force)
    {
        rb.velocity = transform.right * force;
    }

    private void Update()
    {
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Floor":
                StartCoroutine(StopColision());
                break;
            case "Enemy":
                if (used)
                    return;
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.ReceiveDommage(damage);
                rb.velocity = new Vector2(0, 0);
                used = true;
                break;
            case "Player":
                if (used)
                    return;
                isPlaying.instance.addDommage(damage);
                rb.velocity = new Vector2(0, 0);
                used = true;
                break;
        }
    }

    IEnumerator StopColision()
    {
        yield return new WaitForSeconds(0.04f);
        rb.simulated = false;
        yield return new WaitForSeconds(25f);
        Destroy(gameObject);
    }

}