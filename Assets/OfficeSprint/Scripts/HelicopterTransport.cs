// This script was added by Roshan
// Make the helicopter with carpet fly back and forth 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterTransport : MonoBehaviour
{
    // Helicopter Position
    private Vector3 heliPos;

    // Start is called before the first frame update
    void Start()
    {
        heliPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the helicopter fly between 2 buildings
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        float heliMovement = Mathf.PingPong(Time.time * 10.0f, 200.0f) - 100;
        transform.position = heliPos + new Vector3(heliMovement, 0, 0);
    }
}
