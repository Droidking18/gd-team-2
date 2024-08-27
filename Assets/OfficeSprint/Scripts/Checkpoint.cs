// This script was added by Roshan
// Get Gunther's position for UpdateLatestCheckpoint on PlayerScript.cs
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Create a vector3 to store the position of Gunther
    private Vector3 latestCheckpoint;

    private void Start()
    {
        // Store Gunther's position
        latestCheckpoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Updates the checkpoint on PlayerScript.cs when Gunther collides with 
        // the checkpoint box collider. (using the latestCheckpoint in this script)
        if (other.CompareTag("Player"))
        {
            PlayerScript playerScript = other.GetComponent<PlayerScript>();
            if (playerScript != null)
            {
                // Updates the checkpoint on PlayerScript.cs
                playerScript.UpdateLatestCheckpoint(latestCheckpoint);
            }
        }
    }
}
