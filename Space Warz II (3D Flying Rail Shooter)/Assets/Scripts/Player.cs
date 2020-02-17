using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("in m/s")][SerializeField] float _speed = 10f;

    [SerializeField] private float _xPositionBoundary = 5f;
    [SerializeField] private float _yPositionBoundary = 3f;
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float constrainedXPosition = CalculateHorizontalMovement();

        float constrainedYPosition = CalculateVerticalMovement();

        float zPosition = transform.localPosition.z;

        transform.localPosition = new Vector3(constrainedXPosition, constrainedYPosition, zPosition);
    }

    private float CalculateHorizontalMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalOffsetForFrame = horizontalInput * _speed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + horizontalOffsetForFrame;
        float constrainedXPosition = Mathf.Clamp(rawXPosition, -_xPositionBoundary, _xPositionBoundary); // restricting the position so that the player cannot fly off the screen
        return constrainedXPosition;
    }

    private float CalculateVerticalMovement()
    {
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");
        float verticalOffsetForFrame = verticalInput * _speed * Time.deltaTime;

        float rawYosition = transform.localPosition.y + verticalOffsetForFrame;
        float constrainedYPosition = Mathf.Clamp(rawYosition, -_yPositionBoundary, _yPositionBoundary); // restricting the position so that the player cannot fly off the screen
        return constrainedYPosition;
    }
}
