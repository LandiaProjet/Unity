using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{

    public Animator anim;
    public float speed = 2f;
 
    [HideInInspector]
    public bool hasTarget = false; 
    [HideInInspector]
    public GameObject target;  
 
    private Rigidbody2D rb;

 
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }
 
    private void FixedUpdate() {
        if (hasTarget) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 2) {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                anim.SetBool("canWalk", true);
                anim.SetBool("Attack", false);
            } else {
                anim.SetBool("canWalk", false);
                anim.SetBool("Attack", true);
            }
            if(target.transform.position.x > transform.position.x){
                transform.localScale = new Vector3(1,1,1);
            }else if(target.transform.position.x < transform.position.x){
                transform.localScale = new Vector3(-1,1,1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {  
            target = collision.gameObject;     
            hasTarget = true;   
        }
    }
 
 
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            target = null;
            hasTarget = false;
        }
    }
 

}