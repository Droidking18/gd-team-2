// Sound effect for Gunther when he falls on a ground after jumping
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casual_Male_K : MonoBehaviour
{
    private AudioSource audioSource;
    // Get the ground layer
    [SerializeField] private LayerMask groundLayer;
    private bool timeDelay = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Time delay to avoid sounds from playing too often moving on uneven surface
        // The following ground layer detection line of the code was obtained from the discussion forum on unity.
        // https://discussions.unity.com/t/ontriggerenter-layers-vs-tags/903305/7
        if (!timeDelay && (groundLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            // Play the sound effect when Gunther collides with the ground layer
            audioSource.Play();
            // Set timeDelay to false to prevent repetitive sounds
            timeDelay = true;
            // Reset the time delay after 1 second
            StartCoroutine(ResetTimeDelay());
        }
    }

    private IEnumerator ResetTimeDelay()
    {
        // Allow sound effect to be played after 1 second
        yield return new WaitForSeconds(1f);
        timeDelay = false;
    }
}
