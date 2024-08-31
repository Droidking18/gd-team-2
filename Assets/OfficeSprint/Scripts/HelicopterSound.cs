// Play audio when Gunther jumps on the transport helicopter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    // Audio renamed to heliSound to avoid confusion with multiple audioSource in Player object
    public AudioSource heliSound;

    private void OnCollisionEnter(Collision other)
    {
        // Play the helicopter sound when Gunther collides with helicopter
        if (other.gameObject.CompareTag("TransportHelicopter"))
        {
            heliSound.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("TransportHelicopter"))
        {
            // Stop the helicopter sound when Gunther does not collide with helicopter
            heliSound.Stop();
        }
    }
}
