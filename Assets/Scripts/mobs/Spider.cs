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

    public float damage = 1f;

    public BoxCollider2D boxCollider2D;

    private bool cooling;
    private float intTimer;
    public float timer;
 
    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        intTimer = timer;
    }
 
    private void FixedUpdate() {
        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
        if (hasTarget) {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance > 2) {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                anim.SetBool("canWalk", true);
                StopAttack();
            } else {
                anim.SetBool("canWalk", false);
                if(!cooling)
                    Attack();
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
    
    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    private void StopAttack()
    {
        cooling = false;
        anim.SetBool("Attack", false);
    }
    
    private void Attack()
    {
        timer = intTimer;
        
        anim.SetBool("Attack", true);
    }

    private void Damage(){
        if(boxCollider2D.IsTouching(target.GetComponent<Collider2D>())){
            isPlaying.instance.addDommage(this.damage);
            Debug.Log("damage !");
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
    
}