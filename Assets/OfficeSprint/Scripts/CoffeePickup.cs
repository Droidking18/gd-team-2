using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//this makes sure that the object has a collider for the script to run 
[RequireComponent(typeof(Collider))]
public class CoffeePickup : MonoBehaviour
{
    [SerializeField]
    private float _speedIncreaseAmount = 5f;
    [SerializeField]
    private float _powerupDuration = 3f;

    [SerializeField]
    private GameObject _artToDisable = null;

    private Collider _collider;

    // The following code was added by Roshan
    [SerializeField]
    private TextMeshProUGUI powerUpText;

    // Origin position of the coffee can (for floating movement)
    private Vector3 coffeeStartPosition;

    // Audio when Gunther collides with coffee can
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Coffee can movement
        coffeeStartPosition = transform.position;
        // Get audio source
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        // Make the coffee can float
        float coffeeMovement = Mathf.PingPong(Time.time * 3.0f, 5.0f) - 1;
        transform.position = coffeeStartPosition + new Vector3(coffeeMovement, 0, 0);
    }
    // End of code written by Roshan


    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        // The following code was added by Roshan
        if (other.CompareTag("casual_Male_K"))
        {
            // Play the audio
            audioSource.Play();
            // Display the speed boost text for 2 seconds. Power up is changed to Speed Boost!
            powerUpText.text = "Speed Boost!";
            powerUpText.gameObject.SetActive(true);
            // https://learn.unity.com/tutorial/invoke-2d#5c8a6da9edbc2a067d4752d0
            // https://stackoverflow.com/questions/74870167/the-name-nameof-does-not-exist-in-the-current-context-assembly-csharp
            Invoke(nameof(HideSpeedBoostText), 4f);
        }
        // End of code added by Roshan

        PlayerScript playerscript = other.gameObject.GetComponent<PlayerScript>();
        if (playerscript != null)
        {
            // power up sequence begins here
            StartCoroutine(PowerupSequence(playerscript));
        }         
    }

    // The following code was added by Roshan
    private void HideSpeedBoostText()
    {
        // Hide the speed boost text
        powerUpText.text = "";
        powerUpText.gameObject.SetActive(false);
    }
    // End of code added by Roshan


    public IEnumerator PowerupSequence(PlayerScript playerscript)
    {
        //implement a soft disable
        _collider.enabled = false;
        _artToDisable.SetActive(false);

        ActivatePowerup(playerscript);


        yield return new WaitForSeconds(_powerupDuration);

        DeactivatePowerup(playerscript);
         

        Destroy(gameObject);
    }


    private void ActivatePowerup(PlayerScript playerscript)
    {
        playerscript.SetMoveSpeed(_speedIncreaseAmount);
    }

    private void DeactivatePowerup(PlayerScript playerscript)
    {
        playerscript.SetMoveSpeed(-_speedIncreaseAmount);
    }
}