using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookY : MonoBehaviour
{
    private float _sensitivity = 1.0f;

    void Update()
    {

        float mouseY = Input.GetAxis("Mouse Y");

        float rotation = mouseY * _sensitivity;

        Vector3 newRotation = transform.localEulerAngles;

        newRotation.x -= rotation;

        transform.localEulerAngles = newRotation;
    }
}
