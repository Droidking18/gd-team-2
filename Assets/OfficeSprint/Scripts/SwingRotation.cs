using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingRotation : MonoBehaviour
{
    public float rotationSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
