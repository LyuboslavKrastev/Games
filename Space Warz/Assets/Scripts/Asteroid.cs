using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private float _speed = 25;

    [SerializeField]
    private GameObject _explosionPrefab;
    
    [SerializeField]
    private SpawnManager _spawnManager;

    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _speed) * Time.deltaTime);
    }

    // Check for laser colission (Trigger)
    // instantiate explosion at current position
    // destroy explosion after 3 seconds

    private void OnTriggerEnter2D(Collider2D other) // we can find out who collided with us
    {
        if (other.gameObject.tag == "Laser")
        {
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.4f);
            Destroy(other.gameObject);

            GameObject explosion =  Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(explosion, 3.0f);
        }
    }
}
