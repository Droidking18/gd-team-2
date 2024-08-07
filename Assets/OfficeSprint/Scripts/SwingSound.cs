// Added by Roshan
// Handle the audio when Gunther jumps on a swing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSound : MonoBehaviour
{
    public AudioSource swingSound; 

    // Start is called before the first frame update
    // The following code was learnt from the following webpage:
    // https://discussions.unity.com/t/audiosource-play-problem/909074
    void Start()
    {
        if (swingSound != null)
        {
            //Debug.LogError("Swing sound works");
        }
        else
        {
            //Debug.Log("Swing sound error");
        }
    }

    // https://discussions.unity.com/t/problems-with-private-void-oncollisionenter-collision-collision/882528
    // https://discussions.unity.com/t/issue-with-oncollisionenter-not-being-called/593813
    // https://stackoverflow.com/questions/67553565/what-decides-the-calling-order-of-oncollisionenterother-in-unity
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Swing") && swingSound != null)
        {
            //Debug.Log("Collision entry");

            // Debug.Log("Start swing sound");
            swingSound.Play();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Swing") && swingSound != null)
        {
            //Debug.Log("Collision exit");

            //Debug.Log("Stop swing sound");
            swingSound.Stop();
        }
    }
}
