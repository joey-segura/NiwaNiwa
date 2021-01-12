using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float CAMERASPEED = 0.3f;
    public Vector3 offset;

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, CAMERASPEED);
        transform.position = new Vector3(transform.position.x, transform.position.y, smoothedPosition.z);
    }
}
