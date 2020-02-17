using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("in m/s")][SerializeField] float _horizontalSpeed = 4f;

    [SerializeField] private float _xPositionBoundary = 5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float horizontalOffsetForFrame = horizontalInput * _horizontalSpeed * Time.deltaTime;

        float rawXPosition = transform.localPosition.x + horizontalOffsetForFrame;
        float constrainedXPosition = Mathf.Clamp(rawXPosition, -_xPositionBoundary, _xPositionBoundary); // restricting the position so that the player cannot fly off the screen
        float yPosition = transform.localPosition.y;
        float zPosition = transform.localPosition.z;

        transform.localPosition = new Vector3(constrainedXPosition, yPosition, zPosition);
    }
}
