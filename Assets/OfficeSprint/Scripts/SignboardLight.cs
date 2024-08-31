// Turns the beacon light off and on
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignboardLight : MonoBehaviour
{
    public Light signBoard;

    // Start is called before the first frame update
    void Start()
    {
        // Turn on and off the spot light every 1-2 seconds
        StartCoroutine(Signboard());
    }

    IEnumerator Signboard()
    {
        while (true)
        {
            // The following code was learnt on from the webpage below:
            // https://discussions.unity.com/t/turn-on-and-off-lights-with-timer/834494
            signBoard.enabled = true; 
            yield return new WaitForSeconds(Random.Range(1,2));
            signBoard.enabled = false; 
            yield return new WaitForSeconds(Random.Range(1,1));
        }
    }
}
