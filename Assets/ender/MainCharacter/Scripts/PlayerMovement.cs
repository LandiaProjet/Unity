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

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    private float horizontalMovement;
    private Vector3 velocity;
    private bool isJumping;
    public bool isGrounded;

    private float lastPosY = 0;

    void Start()
    {
        Flip(rb.velocity.x);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") == true && isGrounded == true)
        {
            isJumping = true;
        }

        Flip(rb.velocity.x);

        float characterVelocityX = Mathf.Abs(rb.velocity.x);
        float characterVelocityY = Mathf.Abs(rb.velocity.y);
        if (characterVelocityY > 0.2)
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

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

        MovePlayer(horizontalMovement);
        Debug.Log(message: transform.position.y + " " + lastPosY);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumping == true && isGrounded == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
