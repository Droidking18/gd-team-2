using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    //Character movement speed
    public float movementSpeed = 5.0f;
    //Character rotation speed 
    public float rotationSpeed = 5.0f;
    // Character jump height
    public float jumpHeight = 4f;

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
    //Step offset to from CController
    private float ccStepOffset;

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
        // Gets character controller step offset
        ccStepOffset = controller.stepOffset;

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var meshRenderer = hit.gameObject.GetComponent<MeshRenderer>();
        Debug.Log("Building color hit registered");
        if (hit.gameObject.CompareTag("Building"))
        {
            meshRenderer.material.color = Color.red; // Change the building's color
        }
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        PlayerMovement();

        // Apply gravity if not grounded
        if (!controller.isGrounded)
        {
            ccStepOffset = 
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            controller.stepOffset = ccStepOffset;
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
            //transform.Rotate(0f, horizontal * rotationSpeed * Time.deltaTime, 0f);
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime); 

        }

        // Jumping (only when grounded)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Calculate jump velocity based on the jumpHeight
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Animation
        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animator.SetFloat("MovementValue", movementAmount, 0.2f, Time.deltaTime);
    }

   
}