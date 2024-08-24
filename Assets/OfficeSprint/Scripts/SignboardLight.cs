// This script was added by Roshan
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
        // https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
        StartCoroutine(Signboard());
    }

    IEnumerator Signboard()
    {
        while (true)
        {
            // The following code was learnt on from the webpage below:
            // https://discussions.unity.com/t/turn-on-and-off-lights-with-timer/834494
            signBoard.enabled = true; // Shine the spot light on the sign board
            yield return new WaitForSeconds(1);
            signBoard.enabled = false; // Turn off the spot light
            yield return new WaitForSeconds(1);
        }
    }

}
