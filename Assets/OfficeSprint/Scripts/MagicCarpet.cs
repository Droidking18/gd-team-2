// This script was added by Roshan
// Make the magic carpet fly back and forth
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCarpet : MonoBehaviour
{
    // Magic carpet position
    private Vector3 carpetPos;

    // Start is called before the first frame update
    void Start()
    {
        carpetPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Make the magic carpet fly between 2 buildings
        // The following code was learnt from these sources:
        // https://stackoverflow.com/questions/62472719/how-can-i-pingpong-between-two-values-3-and-3-slowly
        // https://stackoverflow.com/questions/61603070/mathf-pingpong-from-1-to-0
        float carpetMovement = Mathf.PingPong(Time.time * 2.0f, 20.0f);
        transform.position = carpetPos + new Vector3(carpetMovement, 0, 0);
    }
}
