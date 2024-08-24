// This script was added by Roshan
// Handle the audio when Gunther jumps on a swing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSound : MonoBehaviour
{
    // Audio renamed to swingSound to avoid confusion with audioSource in Player object
    public AudioSource swingSound;

    // https://discussions.unity.com/t/problems-with-private-void-oncollisionenter-collision-collision/882528
    // https://discussions.unity.com/t/issue-with-oncollisionenter-not-being-called/593813
    // https://stackoverflow.com/questions/67553565/what-decides-the-calling-order-of-oncollisionenterother-in-unity
    private void OnCollisionEnter(Collision other)
    {
        // Play the swing sound when Gunther collides with Swing
        if (other.gameObject.CompareTag("Swing") && swingSound != null)
        {
           swingSound.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        // Stop the swing sound when Gunther does not collides with Swing
        if (other.gameObject.CompareTag("Swing") && swingSound != null)
        {
            swingSound.Stop();
        }
    }
}
