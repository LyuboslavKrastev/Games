using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    // private makes this field inaccessible for other game objects and scripts
    // it is made accessible through the unity inspector, using this attribute
    [SerializeField]
    private float _speed = 3.5f;
    private float _bonusSpeed = 5f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _shieldVisual;

    [SerializeField]
    private GameObject[] engineFires;

    [SerializeField]
    private GameObject _trippleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private float _canFire = -1f;

    private UIManager _uIManager;

    private const float LOWER_BOUND = -3.8f;
    private const float UPPER_BOUND = 0.0f;
    private const float LEFT_BOUND = 11.3f;
    private const float RIGHT_BOUND = -11.3f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score;

    private SpawnManager _spawnManager;

    private bool _isTrippleShotEnabled = false;
    private bool _isShieldEnabled = false;

    private AudioSource _laserSound;

    void Start()
    {
        /* Take the current position and assign it a starting position
           When the game is started, set it to zero (0, 0, 0) */
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn manager not found!");
        }

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager == null)
        {
            Debug.LogError("UI manager not found!");
        }

        _laserSound = GetComponent<AudioSource>();

        if (_laserSound == null)
        {
            Debug.LogError("AudioSource not found!");
        }
    }

    public void EnableShield()
    {
        _isShieldEnabled = true;

        // vizualise shield
        _shieldVisual.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        CalculatePlayerMovement();

        // Spawn a gameObject on space press
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        // Time.time is the time the game has been running
        _canFire = Time.time + _fireRate;
        Vector3 offset = new Vector3(0, 1.0f, 0);

        if (_isTrippleShotEnabled == false)
        {
            // Quaternion.Identity means default rotation
            Instantiate(_laserPrefab, transform.position + offset, Quaternion.identity);
        }
        else
        {
            Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
        }

        _laserSound.Play();
    }

    private void CalculatePlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        /* Time.deltaTime does the conversion from framerate dependent to real time (second, minutes, hours)
        (can be thought of as one second - in this case the object will be moving at _speed meters per second in the direction, determined by the player input) */

        transform.Translate(direction * _speed * Time.deltaTime);

        float playerXPosition = transform.position.x;
        float playerYPosition = transform.position.y;
        ValidatePlayerPosition(playerXPosition, playerYPosition);
    }

    // Sets the game bounds for the player
    private void ValidatePlayerPosition(float xPosition, float yPosition)
    {
        // Check the top and bottom bounds of the game
        if (yPosition >= UPPER_BOUND)
        {
            transform.position = new Vector3(xPosition, UPPER_BOUND, 0);
        }
        else if (yPosition <= LOWER_BOUND)
        {
            transform.position = new Vector3(xPosition, LOWER_BOUND, 0);
        }

        // Add the left and right bounds of the game
        if (xPosition > LEFT_BOUND)
        {
            transform.position = new Vector3(RIGHT_BOUND, yPosition, 0);
        }
        else if (xPosition < RIGHT_BOUND)
        {
            transform.position = new Vector3(LEFT_BOUND, yPosition, 0);
        }
    }

    public void EnableTrippleShot()
    {
        this._isTrippleShotEnabled = true;

        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    private IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        this._isTrippleShotEnabled = false;
    }

    public void EnableSpeedBoost()
    {
        this._speed += this._bonusSpeed;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    private IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        this._speed -= this._bonusSpeed;
    }

    public void TakeDamage()
    {
        if (this._isShieldEnabled == true)
        {
            _isShieldEnabled = false;
            _shieldVisual.SetActive(false);
            return;
        }

        EnableEngineFire();

        this._lives -= 1;

        _uIManager.UpdateLives(_lives);

        if (this._lives < 1)
        {
            Destroy(this.gameObject);

            _uIManager.GameOver();

            // inform spawn manager

            this._spawnManager.OnPlayerDeath();
        }
    }

    private void EnableEngineFire()
    {
        GameObject selection = engineFires
        .Where(i => !i.activeSelf)
        .OrderBy(n => UnityEngine.Random.value)
        .FirstOrDefault();

        // selection will be null if all game objects are already active
        if (selection != null)
        {
            selection.SetActive(true);
        }
    }

    public void IncreaseScore(int points)
    {
        this._score += points;
        this._uIManager.UpdateScore(this._score);
    }
}
