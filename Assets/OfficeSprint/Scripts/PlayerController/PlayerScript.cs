using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    //Character movement speed
    public float movementSpeed = 5.0f;
    //Character rotation speed 
    public float rotationSpeed = 5.0f;
    // Character jump height
    public float jumpHeight = 10f;
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
    private readonly float gravity = -19.81f;

    // Added by Roshan
    // Adjust the number of lives 
    [Header("Lives Remaining")]
    private int livesLeft;
    private int totalLives = 30;

    private bool playFallAudio;

    // End of code added by Roshan


    void Start()
    {
        livesLeft = totalLives;

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

        // Added by Roshan
        // Hide the mouse cursor
        // https://docs.unity3d.com/ScriptReference/Cursor-visible.html
        // https://discussions.unity.com/t/why-is-there-no-way-to-hide-the-mouse-cursor-when-entering-play-mode/918105
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // End of code added by Roshan
    }

    private void Update()
    {
        // Added by Roshan
        // Calls the fall function when Gunther falls
        if (transform.position.y < 10)
        {
            fall();
        }
        // End of code added by Roshan

        // Update grounded status and track if it changed
        wasPreviouslyGrounded = isGrounded;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Player movement previous to applying gravity
        PlayerMovement();

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            ccStepOffset = 0;
            velocity.y += (gravity * 2) * Time.deltaTime;
            coyoteTimeCounter -= Time.deltaTime;
        }
        else
        {
            jumpsRemaining = maxJumps;
            coyoteTimeCounter = coyoteTime;
            if (!wasPreviouslyGrounded)
            {
                velocity.y = 0f;
                // Unfavourable flag used to control jump audio
                playFallAudio = false;

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
                //Debug.Log("Playing walk/run audio");
                audioSource.clip = isRunning ? runAudioClip : walkAudioClip;
                audioSource.Play();
            }
        }
        else if (isFalling)
        {
            if (!playFallAudio)
            {
                audioSource.clip = fallAudioClip;
                audioSource.Play();
                playFallAudio = true; // Reset the flag to allow sound
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

        // Changes by Roshan
        // Move forward or back
        Vector3 forwardMovement = transform.forward * vertical;

        // Movement
        Vector3 moveDirection = Vector3.ClampMagnitude(forwardMovement, 1f);
        controller.Move(moveDirection * movementSpeed * Time.deltaTime);

        // Determine if the player is moving
        isMoving = horizontal != 0f || vertical != 0f;

        // Rotation
        // Modified by Roshan
        // https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        float mouseHorizontal = (rotationSpeed / 30) * Input.GetAxis("Mouse X");
        float keyboardRotation = horizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, mouseHorizontal + keyboardRotation);
        // End of code modified by Roshan

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

    // Added by Roshan
    // Function keeps track of the lives left and transforms Gunther to the starting pos when he falls
    private void fall()
    {
        if (livesLeft > 0)
        {
            // Minus a live whenever Gunther falls
            livesLeft--;
            Debug.Log(livesLeft);

            // Disable the controller for Gunther to put him back to the start pos
            // https://discussions.unity.com/t/character-controller-disable/3444
            controller.enabled = false;

            // Transform Gunther's position to the starting position
            transform.position = new Vector3(10, 37, -30);

            // Enable controller once Gunther is repositioned
            // https://discussions.unity.com/t/character-controller-disable/3444
            // Re-enable the CharacterController after the position change
            controller.enabled = true;
        }
        else
        {
            // To implement a function which deals with game over. 
            Debug.Log("Better luck next time!");
        }
    }
    // End of code added by Roshan
}
