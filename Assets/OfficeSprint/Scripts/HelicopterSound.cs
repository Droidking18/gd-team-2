// This script was added by Roshan
// Handle the audio when Gunther jumps on the transport helicopter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    // Audio renamed to heliSound to avoid confusion with audioSource in Player object
    public AudioSource heliSound;

    
    // https://discussions.unity.com/t/problems-with-private-void-oncollisionenter-collision-collision/882528
    // https://discussions.unity.com/t/issue-with-oncollisionenter-not-being-called/593813
    // https://stackoverflow.com/questions/67553565/what-decides-the-calling-order-of-oncollisionenterother-in-unity
    private void OnCollisionEnter(Collision other)
    {
        // Play the helicopter sound when Gunther collides with helicopter
        if (other.gameObject.CompareTag("TransportHelicopter") && heliSound != null)
        {
            heliSound.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("TransportHelicopter") && heliSound != null)
        {
            // Stop the helicopter sound when Gunther does not collides with helicopter
            heliSound.Stop();
        }
    }
}
