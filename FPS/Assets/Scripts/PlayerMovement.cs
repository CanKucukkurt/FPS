using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    
    public CharacterController controller;
    
    private Vector3 velocity;
    private bool isGrounded;
    private bool wasGrounded;
    
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Store previous grounded state
        wasGrounded = isGrounded;
        
        // Check if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        // Handle landing
        if(isGrounded && !wasGrounded)
        {
            velocity.y = 0f; // Reset vertical velocity on landing
        }
        // Handle ground contact
        else if(isGrounded && velocity.y < 0)
        {
            velocity.y = gravity * Time.deltaTime; // Apply minimal downward force
        }
        
        // Get input axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        // Create movement vector relative to player's rotation
        Vector3 move = transform.right * x + transform.forward * z;
        
        // Apply movement
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        // Handle jumping
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        
        // Apply gravity
        if(!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        
        controller.Move(velocity * Time.deltaTime);
    }
}