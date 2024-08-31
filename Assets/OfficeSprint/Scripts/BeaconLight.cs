// Turns the beacon light off and on
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconLight : MonoBehaviour
{
    public Light beaconLight;

    // Start is called before the first frame update
    void Start()
    {
        // Turn on and off the spot light every 1-2 seconds
        StartCoroutine(Beacon());
    }

    IEnumerator Beacon()
    {
        while (true)
        {
            // The following code was learnt on from the webpage below:
            // https://discussions.unity.com/t/turn-on-and-off-lights-with-timer/834494
            beaconLight.enabled = true; 
            yield return new WaitForSeconds(Random.Range(1, 2));
            beaconLight.enabled = false; 
            yield return new WaitForSeconds(Random.Range(1, 2));
        }
    }
}
