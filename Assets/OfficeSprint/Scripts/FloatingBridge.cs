// This script was added by Roshan
// Plays the sounds when Gunther jumps on the bridge
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBridge : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // Get audio source
        audioSource = GetComponent<AudioSource>();
    }
    // https://discussions.unity.com/t/problems-with-private-void-oncollisionenter-collision-collision/882528
    // https://discussions.unity.com/t/issue-with-oncollisionenter-not-being-called/593813
    // https://stackoverflow.com/questions/67553565/what-decides-the-calling-order-of-oncollisionenterother-in-unity


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("casual_Male_K"))
        {
            audioSource.Play();
        }
     }
}
