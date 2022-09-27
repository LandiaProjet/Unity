using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D rb;
    public BoxCollider2D box;
    public GameObject owner;
    public float damage;

    private float angle;
    private bool used;

    public bool isPlayer;

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
                StartCoroutine(StopColision(0.04f));
                break;
            case "Enemy":
                if (used)
                    return;
                if (owner == other.gameObject)
                    return;
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.ReceiveDommage(damage);
                used = true;
                break;
            case "Player":
                if (used || isPlayer)
                    return;
                Debug.Log("je passes");
                isPlaying.instance.addDommage(damage);
                used = true;
                break;
            default:
                StartCoroutine(StopColision(0.01f));
                break;
        }
    }

    IEnumerator StopColision(float time)
    {
        yield return new WaitForSeconds(time);
        //rb.simulated = false;
        yield return new WaitForSeconds(25f);
        Destroy(gameObject);
    }

}