// This script was added by Roshan
// Sound effect for Gunther when he falls on a ground after jumping
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casual_Male_K : MonoBehaviour
{
    // Sound effect from audio source
    private AudioSource audioSource;
    // Get the ground layer
    [SerializeField]
    private LayerMask groundLayer;
    // Time delay to avoid sounds from playing too often on uneven surface
    private bool timeDelay = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
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
        // Reset the time delay after 1 second
        yield return new WaitForSeconds(1f);
        // Set time delay to false to allow sound to be played
        timeDelay = false;
    }
}
