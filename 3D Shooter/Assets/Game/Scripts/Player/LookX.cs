using UnityEngine;

public class LookX : MonoBehaviour
{
    private float _sensitivity = 1.0f;
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float localX = transform.localEulerAngles.x;
        float localY = transform.localEulerAngles.y;
        float localZ = transform.localEulerAngles.z;
        float rotation = localY + (mouseX *_sensitivity);
        transform.localEulerAngles = new Vector3(localX, rotation, localZ);
    }
}
