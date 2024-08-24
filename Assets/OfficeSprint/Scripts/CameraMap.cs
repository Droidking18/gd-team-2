using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMap : MonoBehaviour
{
    [SerializeField] private Camera mapCamera;

    private void Start()
    {
        mapCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            mapCamera.enabled = true;
        }
    }
}
