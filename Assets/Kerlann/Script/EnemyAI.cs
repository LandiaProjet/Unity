using System;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform target;
    private Animator anim;
    
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    public float timer;
    
    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        anim.SetBool("Fly", true);
    }

    void UpdatePath()
    {
        if(seeker.IsDone() && target != null)
            seeker.StartPath(rb.position, target.position + new Vector3(0,0.75f,0), OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if(path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
        if (force.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f,1f,1f);
        } else if (force.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        
        float distanceWithPlayer = Vector2.Distance (transform.position, target.transform.position);

        if (distanceWithPlayer < 2.5)
        {
            Attack();
        }
        else
        {
            StopAttack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject.transform;
        }
    }
    
    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }
    
    private void Attack()
    {
        timer = intTimer;
        attackMode = true;
        
        anim.SetBool("Attack", true);
    }
    
        
    public void TriggerCooling()
    {
        cooling = true;
    }
    
    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void TakeDamage()
    {
        //damamge
        
    }
}