// This script was added by Roshan
// Make the swing rotate
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotation : MonoBehaviour
{
    public float rotationSpeed = 3f; 

    // Update is called once per frame
    void Update()
    {
        // Rotate the swing
        // https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
