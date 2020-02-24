using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_MovementHandler : MonoBehaviour
{

    [Header("General")]
    [Tooltip("in m/s")][SerializeField] float _speed = 10f;
    [SerializeField] private float _xPositionBoundary = 8f;
    [SerializeField] private float _yPositionBoundary = 5f;

    [Header("Screen-position based")]
    private float _positionPitchFactor = -5f;
    private float _positionYawFactor = 5f;

    [Header("Control-throw based")]
    private float _controlPitchFactor = -20f;
    private float _controllRollFactor = -20f;

    private float _horizontalInput;
    private float _verticalInput;

    private bool _controllsEnabled = true;
    void Update()
    {
        if (_controllsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
        }     
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControl =  + _verticalInput * _controlPitchFactor;
        float pitch = pitchDueToControl + pitchDueToPosition;

        float yaw = transform.localPosition.x *  _positionYawFactor;

        float roll =  _horizontalInput * _controllRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        float constrainedXPosition = CalculateHorizontalMovement();

        float constrainedYPosition = CalculateVerticalMovement();

        float zPosition = transform.localPosition.z;

        transform.localPosition = new Vector3(constrainedXPosition, constrainedYPosition, zPosition);
    }

    private float CalculateHorizontalMovement()
    {
        _horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalOffsetForFrame = _horizontalInput * _speed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + horizontalOffsetForFrame;
        float constrainedXPosition = Mathf.Clamp(rawXPosition, -_xPositionBoundary, _xPositionBoundary); // restricting the position so that the player cannot fly off the screen
        return constrainedXPosition;
    }

    private float CalculateVerticalMovement()
    {
        _verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        float verticalOffsetForFrame = _verticalInput * _speed * Time.deltaTime;

        float rawYPosition = transform.localPosition.y + verticalOffsetForFrame;
        float constrainedYPosition = Mathf.Clamp(rawYPosition, -_yPositionBoundary, _yPositionBoundary); // restricting the position so that the player cannot fly off the screen
        return constrainedYPosition;
    }

    void OnDeath() // called by string reference
    {
        _controllsEnabled = false;
    }
}
