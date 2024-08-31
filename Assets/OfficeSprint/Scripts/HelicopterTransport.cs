// Make the helicopter with carpet fly back and forth between selected buildings
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterTransport : MonoBehaviour
{
    private Vector3 heliPos;
    private float heliMovement;

    // Start is called before the first frame update
    void Start()
    {
        heliPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the helicopter fly between 2 buildings
        heliMovement = Mathf.PingPong(Time.time * 10.0f, 200.0f) - 100;
        transform.position = heliPos + new Vector3(heliMovement, 0, 0);
    }
}
