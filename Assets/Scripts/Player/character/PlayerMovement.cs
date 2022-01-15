using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool isAttack;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;
    public bool isGrounded;

    private float horizontalMovement;
    private Vector3 velocity;
    private bool isJumping;
    private bool isRoll;
    private bool isDie = false;


    private float lastPosY = 0;

    public MovementJoystick movementJoystick;

    public static PlayerMovement instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scène");
            return;
        }

        instance = this;
    }

    void Start()
    {
        Flip(rb.velocity.x);
    }

    public void Jump(){
        if (isGrounded == true && isRoll == false)
            isJumping = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) == true)
        {
            setDie(true);
        }
        if (!isDie && !isAttack)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) == true && isGrounded == true)
            {
                isRoll = true;
                animator.SetBool("isRoll", isRoll);
                StartCoroutine(waitRoll());
            }
            if (Input.GetButtonDown("Jump") == true && isGrounded == true && isRoll == false)
                isJumping = true;

            Flip(rb.velocity.x);

            float characterVelocityX = Mathf.Abs(rb.velocity.x);
            float characterVelocityY = Mathf.Abs(rb.velocity.y);
            if (characterVelocityY > 0.2 && isRoll == false)
            {
                if (lastPosY < transform.position.y && isGrounded == false)
                    animator.SetBool("isRise", true);
                else
                    animator.SetBool("isRise", false);
                animator.SetBool("isFall", !isGrounded);
                lastPosY = transform.position.y;
            }
            animator.SetFloat("Speed", characterVelocityX);
        }
    }

    void FixedUpdate()
    {
        if (!isDie && !isAttack)
        {
             if(movementJoystick.joystickVec.y != 0)
            {
                horizontalMovement = movementJoystick.joystickVec.x * moveSpeed * Time.deltaTime;
            }
            else
            {
                horizontalMovement = 0;
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
            float h = Input.GetAxis("Horizontal");
            if(h != 0){
                MovePlayer(h * moveSpeed); 
            } else {
               MovePlayer(movementJoystick.joystickVec.x * moveSpeed); 
            }
        }
        
    }

    void MovePlayer(float _horizontalMovement)
    {

        if (isRoll == false)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
            if (isJumping == true && isGrounded == true)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        } else {
            float velocityRoll = (spriteRenderer.flipX) ? -10 : 10;
            Vector3 targetVelocity = new Vector2(velocityRoll, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    
    }

    void Flip(float _velocityX)
    {
        if (_velocityX > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocityX < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator waitRoll()
    {
        yield return new WaitForSeconds(0.40f);
        isRoll = false;
        animator.SetBool("isRoll", false);
    }

    public void setDie(bool value)
    {
        rb.velocity = new Vector2(0, 0);
        isDie = value;
        animator.SetBool("isDie", value);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
