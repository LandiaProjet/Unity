using System;
using System.Collections;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;

    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    #endregion
    
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;


    public bool isGrounded;

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
        
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    
    private void Update()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }
        //quand le joueur est detecter
        if (hit.collider != null)
        {
            EnemyLogic();
        } else if (hit.collider == null)
        {
            inRange = false;
        }

        if (inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();
        }
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

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
        {
            Move();
            StopAttack();
        } else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    private void Move()
    {
        anim.SetBool("canWalk", true);
    }

    private void Attack()
    {
        timer = intTimer;
        attackMode = true;
        
        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    private void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector3.left * rayCastLength, Color.red);
        } else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector3.left * rayCastLength, Color.green);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.gameObject.transform;
            inRange = true;
            Debug.Log("test");
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
    
    
    void FixedUpdate()
    {
        if(path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + 0.1f);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        
        if (isGrounded)
        {
            if (direction.y > 0.8f)
            {
                rb.AddForce(Vector2.up * speed * 0.5f);
            }
        }
        
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
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
}