using UnityEngine;

public class CreateCollidersChildren : MonoBehaviour
{
    void Awake()
    {
        var childrenColliders = GetComponentsInChildren<MeshRenderer>();
        foreach (var child in childrenColliders)
        {
            if (!child.GetComponent("BoxCollider"))
            {
                child.gameObject.AddComponent<MeshCollider>();
            }
        }
    }
}