using UnityEngine;

public class PlayerScriptCC : MonoBehaviour
{
    [Header("Player Movement")]
    //Character movement speed
    public float movementSpeed = 5.0f;
    //Character rotation speed 
    public float rotationSpeed = 5.0f;
    // Character jump height
    public float jumpHeight = 8f;

    // Animator component
    [Header("Player animator")]
    public Animator animator;

    // Gravity and ground check for jump
    [Header("Player controller")]
    // Character controller component
    public CharacterController characterController;

    void Start()
    {
        // Gets character controller component
        characterController = GetComponent<CharacterController>();
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
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * movementSpeed;
        movementDirection.Normalize();

        characterController.SimpleMove(movementDirection * magnitude);

        if(movementDirection != Vector3.zero)
        {
            Quaternion forRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, forRotation, rotationSpeed * Time.deltaTime);
        }

        // Animation
        float movementAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animator.SetFloat("MovementValue", movementAmount, 0.2f, Time.deltaTime);
    }


}