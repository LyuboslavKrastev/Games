using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 8.0f;
    private float offScreenYPosition = 8.0f;

    // Update is called once per frame
    void Update()
    {
        CalculateLaserMovement();
    }

    private void CalculateLaserMovement()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        if (transform.position.y > offScreenYPosition) // it has gone off-screen
        {
            Destroy(this.gameObject);
        }
    }
}
