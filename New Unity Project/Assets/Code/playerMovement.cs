using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float movespeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;

    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;

    //Awake is called after objects
    private void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        jumpCount = maxJumpCount;
    }
    // Update is called once per frame
    void Update()
    {
        ProcessInputs();

        Animate();
    }
    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects);
        if(isGrounded)
        {
            jumpCount = maxJumpCount;
        }
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * movespeed, rb.velocity.y);
        if(isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
        isJumping = false;
    }
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
