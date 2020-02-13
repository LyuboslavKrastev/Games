using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (rigidbody == null)
        {
            Debug.LogError("RigidBody is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            Vector3 force = Vector3.up;
            rigidbody.AddRelativeForce(force * Time.deltaTime);
            Debug.Log("thrusting");
        }

        if (Input.GetKey(KeyCode.A)) // cant rotate in both directions
        {
            // rotate left
            transform.Rotate(Vector3.forward); // Z axis
            Debug.Log("rotating left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotate right
            transform.Rotate(-Vector3.forward);
            Debug.Log("rotating right");
        }
    }
}
