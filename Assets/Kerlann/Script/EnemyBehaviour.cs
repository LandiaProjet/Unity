using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    
    public float timer;

    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    #endregion
    
    public Transform target;

    public float speed = 220f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    
    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
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
    
    void FixedUpdate()
    {
        Cooldown();
        if(path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            Attack();
            reachedEndOfPath = true;
            return;
        }
        else
        {
            StopAttack();
            reachedEndOfPath = false;
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
    }
    
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    
    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position + new Vector3(0,1f,0), OnPathComplete);
    }

    
}