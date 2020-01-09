using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 3.0f;
    private float offScreenYPosition = -5.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= offScreenYPosition)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // we can find out who collided with us
    {
        if (other.gameObject.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.EnableTrippleShot();
            }

            Destroy(this.gameObject);         
        }
    }
}
