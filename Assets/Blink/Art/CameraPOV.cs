using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Das ist dein Spieler
    public Vector3 offset;        // Abstand zur Kamera
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
