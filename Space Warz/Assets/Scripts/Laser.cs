using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 8.0f;
    private float offScreenYPosition = 8.0f;
    private bool _isEnemyLaser = false;

    // Update is called once per frame
    void Update()
    {
        CalculateLaserMovement();
    }

    private void CalculateLaserMovement()
    {
        Vector3 direction = _isEnemyLaser ? Vector3.down : Vector3.up;

        MoveLaser(direction);
    }

    private void MoveLaser(Vector3 direction)
    {
        transform.Translate(direction * _speed * Time.deltaTime);
        DestroyIfOffscreen();
    }

    private void DestroyIfOffscreen()
    {
        float positionY = transform.position.y;
        bool isOffscreen = positionY > offScreenYPosition || positionY < -offScreenYPosition;

        if (isOffscreen) // the laser has gone offscreen
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _isEnemyLaser)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage();
                Destroy(this.gameObject);
            }
        }
    }

    public void SetEnemyLaser()
    {
        _isEnemyLaser = true;
    }
}
