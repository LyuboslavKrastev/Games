using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 0.5f;

    private float offScreenYPosition = -5.0f;
    private float topScreenYPosition = 7.0f;

    private const float LEFT_BOUND = 11.3f;
    private const float RIGHT_BOUND = -11.3f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= offScreenYPosition )
        {
            float randomXPosition = Random.Range(RIGHT_BOUND, LEFT_BOUND);
            transform.position = new Vector3(randomXPosition, topScreenYPosition, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // we can find out who collided with us
    {
        Debug.Log("Hit: " + other.transform.name);
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);

            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage();
            }
        }
        else
        { 
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
