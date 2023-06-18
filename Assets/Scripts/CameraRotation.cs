using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target;  // The game object to follow
    public float distance = 10f;  // Distance from the target
    public float sensitivity = 2f;  // Rotation sensitivity
    public float smoothSpeed = 10f;  // Smoothing speed for camera movement

    private Vector3 targetPosition;
    private float currentRotationX = 0f;
    private float currentRotationY = 0f;
    private bool isRotating = false;

    private void LateUpdate()
    {
        if (target == null)
            return;

        // Calculate the target position
        targetPosition = target.position - transform.forward * distance;

        // Smoothly move the camera towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Rotate the camera around the target when right-clicking
        if (Input.GetMouseButtonDown(1))
        {
            isRotating = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            currentRotationX += Input.GetAxis("Mouse X") * sensitivity;
            currentRotationY -= Input.GetAxis("Mouse Y") * sensitivity;

            // Limit the vertical rotation angle to avoid flipping the camera
            currentRotationY = Mathf.Clamp(currentRotationY, -90f, 90f);

            Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, smoothSpeed * Time.deltaTime);
        }
    }
}


