// This script was added by Roshan
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
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
        StartCoroutine(Beacon());
    }

    IEnumerator Beacon()
    {
        while (true)
        {
            // The following code was learnt on from the webpage below:
            // https://discussions.unity.com/t/turn-on-and-off-lights-with-timer/834494
            beaconLight.enabled = true; // Turn the point light on
            yield return new WaitForSeconds(1);
            beaconLight.enabled = false; // Turn the point light off
            yield return new WaitForSeconds(2);
        }
    }
}
