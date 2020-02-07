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

    private Player _player;
    private Animator _animator;

    private AudioSource _explosionSound;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if (_player == null)
        {
            Debug.LogError("Player not found!");
        }

        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.Log("Animator not found!");
        }

        _explosionSound = GetComponent<AudioSource>();

        if (_explosionSound == null)
        {
            Debug.LogError("AudioSource not found!");
        }
    }

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
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 2.7f); // the animation is 2.5 seconds long so we destroy it after it has finished

            if (_player != null)
            {
                _player.TakeDamage();
            }
        }
        else if(other.gameObject.tag == "Laser")
        {
            if (_player != null)
            {
                _player.IncreaseScore(Random.Range(5, 15));
            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
           
            Destroy(this.gameObject, 2.7f);
            Destroy(other.gameObject);
        }

        _explosionSound.Play();
    }
}
