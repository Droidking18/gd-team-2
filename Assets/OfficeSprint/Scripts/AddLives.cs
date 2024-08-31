using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AddLives : MonoBehaviour
{
    [SerializeField]
    private GameObject artToDisable = null;
    private Collider collider1;

    // Sound when Gunther collides with the heart
    [SerializeField] private AudioSource pickupSound;
    private Vector3 heartStartPosition;
    // Rotational speed of the heart
    public float rotationSpeed = 60f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        heartStartPosition = transform.position;
    }

    void Update()
    {
        float heartMovement = Mathf.PingPong(Time.time * 5.0f, 5.0f) - 1;
        transform.position = heartStartPosition + new Vector3(0, heartMovement * 0.5f, 0);
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        collider1 = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("casual_Male_K"))
        {
            audioSource.Play();
        }

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
        AddLive(playerscript); 
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void AddLive(PlayerScript playerScript)
    {
        // Add one live 
        playerScript.AddLive(1);
    }
}


