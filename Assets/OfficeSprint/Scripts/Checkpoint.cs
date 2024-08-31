// Get Gunther's position for UpdateLatestCheckpoint on PlayerScript.cs
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 latestCheckpoint;

    private void Start()
    {
        // Store Gunther's position when he collides with the most recent checkpoint
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
                // Update the checkpoint on PlayerScript.cs
                playerScript.UpdateLatestCheckpoint(latestCheckpoint);
            }
        }
    }
}
