// Make the magic carpet fly back and forth between selected buildings
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCarpet : MonoBehaviour
{
    private Vector3 carpetPos;
    private float carpetMovement;

    // Start is called before the first frame update
    void Start()
    {
        carpetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the magic carpet fly between 2 buildings
        carpetMovement = Mathf.PingPong(Time.time * 2.0f, 20.0f);
        transform.position = carpetPos + new Vector3(carpetMovement, 0, 0);
    }
}
