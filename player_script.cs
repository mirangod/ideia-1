using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    [Header("Public Player Var")]
    public float moveSpeed;
    public float maxJumpForce = 10f;
    public float minJumpForce = 5f;
    public float maxJumpTime = 0.5f;

    private bool isJumping = false;
    private float jumpTimeCounter;

    [Header("Check Ground")]
    public bool isGrounded;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.velocity = new Vector2(rb.velocity.x, minJumpForce);
        }

        // Se a tecla de pulo estiver sendo mantida pressionada e o jogador ainda estiver pulando
        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Se a tecla de pulo for solta
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        } else if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void OnDrawGizmos()
    {
        if (!isGrounded)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
