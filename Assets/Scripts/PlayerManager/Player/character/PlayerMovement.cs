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

    public Material impact;

    private float horizontalMovement;
    private Vector3 velocity;
    private bool isJumping;
    private bool isRoll;
    private bool isDie = false;
    private Color BloodColor = new Color(0.9098f, 0.18431f, 0.00392f, 1);


    private float lastPosY = 0;

    public float maxVelocity;

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
        if (isGrounded == true && isRoll == false && isAttack == false)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = true;
        }
    }

    public void Roll()
    {
        if (isGrounded == true)
        {
            isRoll = true;
            animator.SetBool("isRoll", isRoll);
            StartCoroutine(waitRoll());
            StartCoroutine(ButtonManager.instance.DisableTemporaryRoll());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ScreenCapture.CaptureScreenshot("screenshot.png");
            Debug.Log("A screenshot was taken!");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            Debug.Log(isDie);
            setDie(!isDie);
        }
        if (!isDie && !isAttack)
        {
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
            if(movementJoystick != null && movementJoystick.joystickVec.y != 0)
            {
                horizontalMovement = movementJoystick.joystickVec.x * moveSpeed * Time.deltaTime;
            }
            else
            {
                horizontalMovement = 0;
            }
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
            if (isGrounded == true)
            {
                //Debug.Log(transform.localPosition);
                //isPlaying.instance.lastPoint = transform.localPosition;
                animator.SetBool("isFall", !isGrounded);
            }
            float h = Input.GetAxis("Horizontal");
            if(h != 0){
                MovePlayer(h * moveSpeed); 
            } else {
               MovePlayer(movementJoystick.joystickVec.x * moveSpeed); 
            }
        }
        var v = rb.velocity;
        if(v.sqrMagnitude > (maxVelocity * maxVelocity)){
            rb.velocity = v.normalized * maxVelocity;
        }
    }

    void MovePlayer(float _horizontalMovement)
    {

        if (isRoll == false)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        } else {
            float velocityRoll = (transform.localScale.x == -1) ? -10 : 10;
            Vector3 targetVelocity = new Vector2(velocityRoll, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
    
    }

    void Flip(float _velocityX)
    {
        if (_velocityX > 0.1f)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (_velocityX < -0.1f)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
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
        animator.Play("Player_idle");
    }

    bool HitAnimationIsEnable;

    public void HitAnimation()
    {
        if (HitAnimationIsEnable)
            return;
        HitAnimationIsEnable = true;
        StartCoroutine(TransitionColor());
    }

    private IEnumerator TransitionColor()
    {
        spriteRenderer.color = BloodColor;
        Material before = spriteRenderer.material;
        spriteRenderer.material = impact;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.material = before;
        spriteRenderer.color = Color.white;
        HitAnimationIsEnable = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
