using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float damping = 5f;
    private float xInput;
    private float yInput;

    private CameraRotation cameraController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraController = FindObjectOfType<CameraRotation>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs()
    {
        float rawXInput = Input.GetAxisRaw("Horizontal");
        float rawYInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraController.transform.forward;
        Vector3 cameraRight = cameraController.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection = cameraForward * rawYInput + cameraRight * rawXInput;
        movementDirection.Normalize();

        xInput = movementDirection.x;
        yInput = movementDirection.z;
    }

    private void Move()
    {
        Vector3 movement = new Vector3(xInput, 0f, yInput) * moveSpeed;
        if (movement.magnitude > 0f)
        {
            rb.AddForce(movement);
        }
        else
        {
            rb.velocity = rb.velocity * (1f - damping * Time.fixedDeltaTime);
        }
    }


}

