using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed = 4.0f;

    private float offScreenYPosition = -5.0f;
    private float topScreenYPosition = 7.0f;

    private const float LEFT_BOUND = 11.3f;
    private const float RIGHT_BOUND = -11.3f;

    // Update is called once per frame
    void Update()
    {
        // 
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= offScreenYPosition )
        {
            float randomXPosition = Random.Range(RIGHT_BOUND, LEFT_BOUND);
            transform.position = new Vector3(randomXPosition, topScreenYPosition, 0);
        }
    }

    private void OnTriggerEnter(Collider other) // we can find out who collided with us
    {
        Debug.Log("Hit: " + other.transform.name);
        // if other is player => destroy this object and damage the player
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("I hit a player!");
            Destroy(gameObject);


        }
        // if other is laser => destroy this object and the laser
        else
        {
            Debug.Log("I was hit by a laser!");
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
