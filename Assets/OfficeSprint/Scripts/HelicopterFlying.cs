﻿// Make the helicopter fly (rotate around the Y axis)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterFlying : MonoBehaviour
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
        // Make the helicopter fly using rotation
        // The following code was learnt from StackOverflow:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        heliMovement = Mathf.PingPong(Time.time * 0.01f, 500.0f);
        transform.Rotate(0, -heliMovement, 0);
    }
}
