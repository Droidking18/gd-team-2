using UnityEngine;
using TMPro;
using System.Collections;

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

    // The following code was written by Roshan
    [SerializeField] private AudioClip fallAudioClip;
    [SerializeField] private AudioClip landAudioClip;
    [SerializeField] private AudioClip loseLiveAudioClip;
    // End of code written by Roshan

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

    // The following code was written by Roshan
    // Remaining lives
    private int livesLeft;
    // Total lives in game
    private int totalLives = 30;
    // Lives counter on canvas
    [SerializeField] private TMP_Text livesRemainingText;

    // Falling audio played when Gunther starts to fall
    private bool playFallAudio;
    // Landing audio played when Gunther reaches the ground layer
    private bool playLandAudio;
    // Obtain last checkpoint position for the checkpoint logic
    private Vector3 currentCheckpointPos;
    // Get the countdown
    private Countdown countdown;
    // Game state when paused
    private bool pause;
    // End of code written by Roshan

    void Start()
    {
        // Start at the last stored checkpoint position
        currentCheckpointPos = transform.position; // Added by Roshan
        livesLeft = totalLives;
        // Update the lives remaining from totalLives
        UpdateLivesLeftDisplay(); // Added by Roshan
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

        // The following code was written by Roshan
        // Hide the mouse cursor during the game
        // https://docs.unity3d.com/ScriptReference/Cursor-visible.html
        // https://discussions.unity.com/t/why-is-there-no-way-to-hide-the-mouse-cursor-when-entering-play-mode/918105
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Get the countdown
        countdown = FindObjectOfType<Countdown>();
        // End of code added by Roshan
    }

    private void Update()
    {
        // The following code was written by Roshan
        // Pause the game on load
        if (countdown != null)
        {
            // Set pause based on the state on countdown.gamePaused flag
            pause = countdown.gamePaused;
        }

        // Calls the fall function when Gunther falls
        if (transform.position.y < 10)
        {
            fall();
        }
        // End of code written by Roshan

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
                audioSource.clip = isRunning ? runAudioClip : walkAudioClip;
                audioSource.Play();
            }
        }
        else if (isFalling)
        {
            if (!playFallAudio)
            {
                // Play the falling sound
                audioSource.clip = fallAudioClip;
                audioSource.Play();
                playFallAudio = true; 
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

        // Modified by Roshan
        // Move forward or back
        Vector3 forwardMovement = transform.forward * vertical;

        if (pause)
        {
            Vector3 moveDirection = Vector3.ClampMagnitude(forwardMovement, 1f);
            controller.Move(moveDirection * 0 * Time.deltaTime);
        } else
        {
            Vector3 moveDirection = Vector3.ClampMagnitude(forwardMovement, 1f);
            controller.Move(moveDirection * movementSpeed * Time.deltaTime);
        }
     

        // Determine if the player is moving
        isMoving = horizontal != 0f || vertical != 0f;

        // Modified by Roshan
        // Rotation
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

    // The following code was written/modifief by Roshan
    // Function keeps track of the lives left and transform Gunther to the starting pos when he falls
    private void fall()
    {
        if (livesLeft > 0)
        {
            // Lose a live sound   
            audioSource.clip = loseLiveAudioClip;
            audioSource.Play();

            // Minus a live whenever Gunther falls
            livesLeft--;
            // Update the lives left
            UpdateLivesLeftDisplay();

            // Disable the controller for Gunther to put him back to the start pos
            // https://discussions.unity.com/t/character-controller-disable/3444
            controller.enabled = false;

            // Transform Gunther's position to the latest checkpoint position
            transform.position = currentCheckpointPos;

            // Enable controller once Gunther is repositioned
            // https://discussions.unity.com/t/character-controller-disable/3444
            // Re-enable the CharacterController after the position change
            controller.enabled = true;
        }
        else
        {
            // To implement a function which deals with game over. 
            Debug.Log("Better luck next time!"); // To debug
        }
    }

    private void UpdateLivesLeftDisplay()
    {
       // Display the remaining lives using livesLeft
       livesRemainingText.text = "Lives: " + livesLeft; 
    }
    // End of code added by Roshan

    // Increased movement speed when Gunther collides with the heart
    public void SetMoveSpeed(float newSpeedAdjustment)
    {
        movementSpeed += newSpeedAdjustment;
        StartCoroutine(ResumeNormalSpeed(newSpeedAdjustment)); // Added by Roshan
    }

    // The following code was written by Roshan
    // Resume normal speed after a time delay of 5 seconds
    private IEnumerator ResumeNormalSpeed( float speedAdjustment)
    {
        yield return new WaitForSeconds(5);
        movementSpeed -= speedAdjustment;
    }

    public void AddLive(int addOneLive)
    {
        // Add a live
        livesLeft += addOneLive; 
        if (livesLeft >= totalLives) 
        {
            // Limit the lives left to total lives
            livesLeft = totalLives;
        }
        UpdateLivesLeftDisplay();
    }

    public void UpdateLatestCheckpoint(Vector3 latestCheckpointPos)
    {
        // Set the latest vector 3 checkpoint as current
        currentCheckpointPos = latestCheckpointPos;
    }
    // End of code added by Roshan
}
