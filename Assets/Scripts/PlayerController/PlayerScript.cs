using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    //Character movement speed
    public float movementSpeed = 5f;
    //Character rotation speed 
    public float rotationSpeed = 100f;
    // Character jump height
    public float jumpHeight = 8f;   

    // Animator component
    [Header("Player animator")]
    public Animator animator;

    // Gravity and ground check for jump
    [Header("Player Ground Check")]
    // Character controller component
    public CharacterController controller;
    // Check to see if character is grounded based on a groundCheck gameobject below character feet
    public Transform groundCheck;
    // Ground distance from character
    public float groundDistance = 0.4f;
    // Ground layer
    public LayerMask groundMask;

    // Stores vertical velocity for gravity force
    private Vector3 velocity;
    // Returns true if character is touching the ground
    private bool isGrounded;
    // Standard Earth gravity
    private readonly float gravity = -9.81f; 

    void Start()
    {
        // Gets character controller component
        controller = GetComponent<CharacterController>(); 
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
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f).normalized; 

        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Rotation
        if (horizontal != 0f)
        {
            transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
        }

        // Jumping (only when grounded)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate jump velocity based on desired height
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
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