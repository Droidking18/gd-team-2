using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 offset;
    public GameObject player;
    //Smoothness
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame after Update it should smooth the camera movement
    void LateUpdate() 
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
