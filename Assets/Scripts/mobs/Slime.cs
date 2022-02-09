using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{

    public Animator anim;
    public float speed = 2f;
 
    [HideInInspector]
    public bool hasTarget = false; 
    [HideInInspector]
    public GameObject target;  
 
    private Rigidbody2D rb;

    public float damage = 1f;

    public BoxCollider2D boxCollider2D;
 
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
 
    private void FixedUpdate() {
        if (hasTarget) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            if (distance > 2) {
                anim.SetBool("isRun", true);
            }
            Flip();
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {  
            target = collision.gameObject;     
            hasTarget = true;   
        }
    }

    void Flip()
    {
        if (target.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (target.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }

    private void Damage(){
        if(boxCollider2D.IsTouching(target.GetComponent<Collider2D>())){
            isPlaying.instance.addDommage(this.damage);
            Debug.Log("damage !");
        }
    }

    public override void onDie(){
        anim.SetBool("isDead", true);
    }
}