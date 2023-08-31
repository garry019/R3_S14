using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMixamo : MonoBehaviour
{
    public PlayerStats playerStats;
    public Animator animator;
    private Rigidbody rb;

    private float movementSpeed;
    public float walkingSpeed = 4f;
    public float sprintSpeed = 10f;

    public bool canRun = true;
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    private float jumpForce = 2.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerStats = GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumping = false;
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
        
    }

    private void Update()
    {
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsSprinting", isSprinting);
        animator.SetBool("IsSprinting", isSprinting);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }



        if (Input.GetKey(KeyCode.LeftShift) && playerStats.energyAmount > 0)
        {
            if (canRun)
            {
                playerStats.energyAmount -= 0.2f; //EnergyDinamics
                isSprinting = true;
                movementSpeed = sprintSpeed;
            }
            else
            {
                if (playerStats.energyAmount < 100)
                {
                    playerStats.energyAmount += 0.5f;
                }
            }
            
        }
        else
        {
            canRun = false;
            //EnergyDinamics
            if (playerStats.energyAmount < 100)
            {
                playerStats.energyAmount += 0.5f;
            }
            if (playerStats.energyAmount >= 100)
            {
                canRun = true;
            }

            isSprinting = false;
            movementSpeed = walkingSpeed;
        }

        Vector3 movement = transform.forward * verticalAxis + transform.right * horizontalAxis;
        movement.Normalize();
        transform.position += movement * movementSpeed * Time.deltaTime;

        animator.SetFloat("Vertical", verticalAxis);
        animator.SetFloat("Horizontal", horizontalAxis);
    }

    private void Jump()
    {
        isJumping = true;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
