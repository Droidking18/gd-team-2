// Added by Roshan
// Handle the audio when Gunther jumps on the transport helicopter
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    public AudioSource heliSound;

    // Start is called before the first frame update
    // The following code was learnt from the following webpage:
    // https://discussions.unity.com/t/audiosource-play-problem/909074
    void Start()
    {
        if (heliSound != null)
        {
            // Debug.LogError("Heli sound works");
        }
        else
        {
            // Debug.Log("Heli sound error");
        }
    }

    // https://discussions.unity.com/t/problems-with-private-void-oncollisionenter-collision-collision/882528
    // https://discussions.unity.com/t/issue-with-oncollisionenter-not-being-called/593813
    // https://stackoverflow.com/questions/67553565/what-decides-the-calling-order-of-oncollisionenterother-in-unity
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("TransportHelicopter") && heliSound != null)
        {
            // Debug.Log("Heli Collision entry");

            // Debug.Log("Heli Start swing sound");
            heliSound.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("TransportHelicopter") && heliSound != null)
        {
            // Debug.Log("Heli Collision exit");

            // Debug.Log("Heli Stop swing sound");
            heliSound.Stop();
        }
    }
}
