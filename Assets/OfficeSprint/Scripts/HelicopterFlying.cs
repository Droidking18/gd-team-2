// This script was added by Roshan
// Make the helicopter fly (rotate around the Y axis)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFlying : MonoBehaviour
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
        // Make the helicopter fly using rotation
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        float heliMovement = Mathf.PingPong(Time.time * 0.01f, 500.0f);
        // The following code was learnt from:
        // https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
        transform.Rotate(0, -heliMovement, 0);
    }
}
