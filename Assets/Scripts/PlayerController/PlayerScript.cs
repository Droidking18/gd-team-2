using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float movementSpeed = 5f; // Adjusted for Character Controller
    public float rotationSpeed = 100f; // Adjusted for Character Controller
    public float jumpHeight = 1f;    // Height of the jump

    [Header("Player animator")]
    public Animator animator;

    [Header("Player Ground Check")] // For jump logic
    public Transform groundCheck;   // Point to check if the player is grounded
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public CharacterController controller;
    private Vector3 velocity;         // To store the vertical velocity (for gravity)
    private bool isGrounded;
    private float gravity = -9.81f; // Standard Earth gravity

    void Start()
    {
        controller = GetComponent<CharacterController>(); // Get the Character Controller component
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        Debug.Log("Is Grounded: " + isGrounded);

        PlayerMovement();

        // Apply gravity if not grounded
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        // Move the character controller with the calculated velocity
        controller.Move(velocity * Time.deltaTime);

    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Movement
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);  // Ensure consistent speed diagonally

        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Rotation
        if (horizontal != 0f)
        {
            transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
        }

        // Jumping (only when grounded)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity based on desired height
        }

        // Animation
        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animator.SetFloat("MovementValue", movementAmount, 0.2f, Time.deltaTime);
    }
}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerScript : MonoBehaviour
//{
//    [Header("Player Movement")]
//    public float movementSpeed = 15f;
//    public float rotationSpeed = 300f;
//    public float jumpSpeed = 8f;

//    [Header("Gravity")]
//    public CharacterController CC;

//    [Header("Player animator")]
//    public Animator animator;

//    void Start()
//    {
//        CC = GetComponent<CharacterController>();
//    }

//    private void Update()
//    {
//        PlayerMovement();
//    }


//    void PlayerMovement()
//    {
//        float horizontal = Input.GetAxisRaw("Horizontal");
//        float vertical = Input.GetAxisRaw("Vertical");

//        //Movement amount value will be between 0 and 1 
//        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

//        transform.Translate(new Vector3(0, 0, vertical) * movementSpeed * Time.deltaTime);

//        transform.Rotate(new Vector3(0, horizontal, 0) * rotationSpeed * Time.deltaTime);

//        if (Input.GetButtonDown("Jump") && transform.position.y == -2)
//        {
//            //rigidBody.AddForce(Vector3.up * jumpSpeed);
//            transform.Translate(new Vector3(0, jumpSpeed, 0));
//        }
//        //Animation movement value based on movement amount and delay to reduce slip
//        animator.SetFloat("MovementValue", movementAmount, 0.2f, Time.deltaTime);
//    }

//}