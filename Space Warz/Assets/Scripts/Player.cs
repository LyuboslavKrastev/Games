using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /* Take the current position and assign it a starting position
           When the game is started, set it to zero (0, 0, 0) */
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /* Time.deltaTime does the conversion from framerate dependent to real seconds, minutes, hours 
        (can be thought of as one second - in this case the object will be moving at 5 meters per second to the right) */
        transform.Translate(Vector3.right * 5 * Time.deltaTime);
    }
}
