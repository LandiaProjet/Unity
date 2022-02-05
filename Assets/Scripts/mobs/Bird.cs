using System.Collections;
using System;
using UnityEngine;

public class Bird : MonoBehaviour
{

    Rigidbody2D rb;

    Animator animator;

    SpriteRenderer sprite;

    bool active;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
            StartCoroutine(Actived());
    }

    IEnumerator Actived()
    {
        rb.AddForce(new Vector3(sprite.flipX ? -4f : 4f, 1.5f, 0), ForceMode2D.Impulse);
        animator.SetTrigger("Fly");
        active = true;
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}