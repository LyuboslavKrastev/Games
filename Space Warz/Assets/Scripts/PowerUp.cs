using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 3.0f;
    private float offScreenYPosition = -5.0f;

    // PowerUp IDs
    // 0 - tripple
    // 1 - speed
    // 2 - shield

    [SerializeField]
    private int powerUpId;

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
                switch (this.powerUpId)
                {
                    case 0:
                        player.EnableTrippleShot();
                        break;
                    case 1:
                        player.EnableSpeedBoost();
                        break;
                    case 2:
                        player.EnableShield();
                        break;
                    default:
                        break;
                }
            }

            Destroy(this.gameObject);         
        }
    }
}
