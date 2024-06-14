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
    // This sets a double jump max of 2 jumps
    public int maxJumps = 2; 
    private int jumpsRemaining;
    // Time where player has left the ground and can still perform another jump
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Animator component
    [Header("Player animator")]
    public Animator animator;

    [Header("Player sounds")]
    [SerializeField] private AudioClip walkAudioClip;
    [SerializeField] private AudioClip runAudioClip;
    [SerializeField] private AudioClip fallAudioClip;
    private AudioSource audioSource;

    // Gravity and ground check for jump
    [Header("Player Ground Check")]
    // Character controller component
    public CharacterController controller;

    // Check to see if character is grounded based on groundCheck gameobject below character feet
    public Transform groundCheck;
    // Ground distance from character
    public float groundDistance = 0.4f;
    // Ground layer
    public LayerMask groundMask;
    //Step offset to from CController
    private float ccStepOffset;
    //Checks if the player is moving
    private bool isMoving;
    //Checks if the player was 
    private bool wasPreviouslyGrounded;
    //Checks if character is running
    private bool isRunning;
    //Checks if character is falling from a rooftop
    private bool isFalling;

    // Stores vertical velocity for gravity force
    private Vector3 velocity;
    // Returns true if character is touching the ground
    private bool isGrounded;
    // Standard Earth gravity
    private readonly float gravity = -9.81f;


    void Start()
    {
        // Gets component AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the GameObject.");
        }
        //Initalising jumpsRemaining to max jumps
        jumpsRemaining = maxJumps;
        // Gets character controller component
        controller = GetComponent<CharacterController>();
        // Gets character controller step offset
        ccStepOffset = controller.stepOffset;
    }

    private void Update()
    {
        // Update grounded status and track if it changed
        wasPreviouslyGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Player movement previous to applying gravity
        PlayerMovement();

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            ccStepOffset = 0;
            velocity.y += gravity * Time.deltaTime;
            coyoteTimeCounter -= Time.deltaTime;
        }
        else
        {
            jumpsRemaining = maxJumps;
            coyoteTimeCounter = coyoteTime;
            if (!wasPreviouslyGrounded)
            {
                velocity.y = 0f;
            }
            controller.stepOffset = ccStepOffset;
        }

        controller.Move(velocity * Time.deltaTime);

        // Fall detection logic
        isFalling = !controller.isGrounded && velocity.y < 0;

        // Sound clip logic
        if (isGrounded && isMoving)
        {
            if (!audioSource.isPlaying)
            {
                Debug.Log("Playing walk/run audio");
                audioSource.clip = isRunning ? runAudioClip : walkAudioClip;
                audioSource.Play();
            }
        }
        else if (isFalling)
        {
            if (!audioSource.isPlaying || audioSource.clip != fallAudioClip)
            {
                audioSource.clip = fallAudioClip;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Movement
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f).normalized;

        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Determine if the player is moving
        isMoving = horizontal != 0f || vertical != 0f;

        // Rotation
        if (horizontal != 0f)
        {
            transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);
        }

        // Jumping logic
        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimeCounter > 0))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            jumpsRemaining--;
        }

        // Animation
        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        isRunning = movementAmount > 0.5f;

        animator.SetFloat("MovementValue", movementAmount, 0.2f, Time.deltaTime);
    }
}
