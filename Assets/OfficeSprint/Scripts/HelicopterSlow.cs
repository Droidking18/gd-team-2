// Added by Roshan
// Make the helicopter fly up and down slowly
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSlow : MonoBehaviour
{
    private Vector3 heliPos;

    // Start is called before the first frame update
    void Start()
    {
        heliPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        float heliMovement = Mathf.PingPong(Time.time * 2.0f, 30.0f);
        transform.position = heliPos + new Vector3(0, heliMovement, 0);
    }
}