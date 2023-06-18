using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuck : MonoBehaviour
{
    [SerializeField] KeyCode buttonMash = KeyCode.Space;
    [SerializeField] float mashThreshold = 10f;
    [SerializeField] float unstuckForce = 10f;

    private bool isStuck = false;
    private float mashCounter = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isStuck && collision.gameObject.CompareTag("RockPlayer"))
        {
            isStuck = true;
            mashCounter = 0f;
            Debug.Log("Rock is stuck!");

            // Gets the rock stuck
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePosition;

        }
    }

    private void unstuck()
    {
        Debug.Log("Rock is unstuck");
        // unstucks the rock
        Rigidbody rb = GameObject.FindGameObjectWithTag("RockPlayer").GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;

        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;

        // Apply force to the player in the direction the camera is viewing
        rb.AddForce(cameraForward * unstuckForce, ForceMode.Impulse);

        isStuck = false;
        mashCounter = 0f;

        // Deactivate the object that makes the player stuck
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isStuck)
        {
            if (Input.GetKeyDown(buttonMash))
            {
                mashCounter += 1f;
                if (mashCounter >= mashThreshold)
                {
                    unstuck();
                }
            }
        }
    }

}
