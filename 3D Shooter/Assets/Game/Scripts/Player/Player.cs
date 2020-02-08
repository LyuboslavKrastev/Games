using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    CharacterController _controller;

    private float _speed = 3;
    private float _gravity = 1.2f;
    private float _jumpStrength = 16.0f;
    private float yVelocity = 0;


    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is NULL!");
        }
    }

    void Update()
    {
        CalculateMovment();
    }

    private void CalculateMovment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
        {
            yVelocity = _jumpStrength;
        }

        yVelocity -= _gravity;

        velocity.y = yVelocity; // applying gravity here

        // asign world space values to the velocity so we move based on world space values
        velocity = transform.TransformDirection(velocity); 
        _controller.Move(velocity * Time.deltaTime);
    }
}
