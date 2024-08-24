using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AddLives : MonoBehaviour
{
    [SerializeField]
    private GameObject artToDisable = null;
    private Collider collider1;

    // The following code was written by Roshan
    // Sound when Gunther collides with the heart
    [SerializeField]
    private AudioSource pickupSound = null;
    // Starting position of the floating heart
    private Vector3 heartStartPosition;
    // Rotational speed of the heart
    public float rotationSpeed = 60f;
    // Sound when Gunther collides with the heart
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // End of code written by Roshan
        heartStartPosition = transform.position;
    }

    void Update()
    {
        // The following code was written by Roshan
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        float heartMovement = Mathf.PingPong(Time.time * 5.0f, 5.0f) - 1;
        transform.position = heartStartPosition + new Vector3(0, heartMovement * 0.5f, 0);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        // End of code written by Roshan
    }

    private void Awake()
    {
        collider1 = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // The following code was written by Roshan
        if (other.CompareTag("casual_Male_K"))
        {
            audioSource.Play();
        }
        // End of code written by Roshan

        PlayerScript playerscript = other.gameObject.GetComponent<PlayerScript>();
        if (playerscript != null)
        {
            // Start the power up sequence
            StartCoroutine(HeartPickupSequence(playerscript));
        }
    }

    private IEnumerator HeartPickupSequence(PlayerScript playerscript)
    {
        //implement a soft disable
        collider1.enabled = false;
        artToDisable.SetActive(false);
        AddLive(playerscript); // Added by Roshan
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // The following code was written by Roshan
    private void AddLive(PlayerScript playerScript)
    {
        // Add one live 
        playerScript.AddLive(1);
    }
    // End of code written by Roshan
}


