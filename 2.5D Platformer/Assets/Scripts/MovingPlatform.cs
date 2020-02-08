using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform pointA;

    [SerializeField]
    private Transform pointB;

    private float _speed = 3.0f;

    private bool _switchDirection = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 currentPosition = transform.position;
        if (_switchDirection == false)
        {
            // move to b
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, Time.deltaTime * _speed);
        }
        else
        {
            // move to a
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, Time.deltaTime * _speed);
        }

        if (transform.position == pointB.position)
        {
            _switchDirection = true;
        }
        else if (transform.position == pointA.position)
        {
            _switchDirection = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Detected player");
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = null;
        }
    }
}
