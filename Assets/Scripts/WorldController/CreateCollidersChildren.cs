using UnityEngine;

public class CreateCollidersChildren : MonoBehaviour
{
    void Awake()
    {
        var childrenColliders = GetComponentsInChildren<MeshRenderer>();
        foreach(var child in childrenColliders)
        {
            child.gameObject.AddComponent<BoxCollider>();
        }       
        
    }

    
}