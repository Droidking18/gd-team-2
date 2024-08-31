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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("casual_Male_K"))
        {
            audioSource.Play();
        }
     }
}
