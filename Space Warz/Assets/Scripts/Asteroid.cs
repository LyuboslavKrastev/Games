using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 25;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _speed) * Time.deltaTime);
    }
}
