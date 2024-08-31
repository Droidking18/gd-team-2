// Make the swing rotate
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotation : MonoBehaviour
{
    public float swingRotation = 3f; 

    // Update is called once per frame
    void Update()
    {
        // Rotate the swing
        transform.Rotate(Vector3.up, swingRotation * Time.deltaTime);
    }
}
