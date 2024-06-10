using UnityEngine;

public class BuildingColorChange : MonoBehaviour
{
    public Color newColor = Color.red; // Set the desired new color in the Inspector

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>(); // Get reference to the Mesh Renderer
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Building color hit registered");
        if (hit.gameObject.CompareTag("Building")) 
        {
            meshRenderer.material.color = newColor; // Change the building's color
        }
    }
}
