using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;             // Objek yang akan diikuti (Player)
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Posisi relatif kamera ke player
    public float smoothSpeed = 0.05f;   // Kehalusan gerakan (0.05 - 0.3 biasanya bagus)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // Hitung posisi kamera yang diinginkan
        Vector3 desiredPosition = target.position + offset;

        // Smooth bergerak ke posisi target
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
