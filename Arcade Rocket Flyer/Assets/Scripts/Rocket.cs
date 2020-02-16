using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private AudioSource _audioSoruce;

    [SerializeField] private float _levelLoadDelay = 2f;

    [SerializeField] private float _rotationPower = 1000f;

    [SerializeField] private AudioClip _mainEngine;
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _success;

    [SerializeField] private ParticleSystem _mainEngineParticles;
    [SerializeField] private ParticleSystem _explosionParticles;
    [SerializeField] private ParticleSystem _successParticles;

    private enum State { Alive, Dying, Transcending }
    private State _state = State.Alive;

    [SerializeField]
    private float _thrustPower = 1700f;
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        if (_rigidBody == null)
        {
            Debug.LogError("RigidBody is NULL!");
        }

        _audioSoruce = GetComponent<AudioSource>();

        if (_audioSoruce == null)
        {
            Debug.LogError("AudioSource is NULL!");
        }
    }

    void Update()
    {
        if (_state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }  
    }
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
            _mainEngineParticles.Play();
            if (!_audioSoruce.isPlaying) // so it does not layer
            {
                _audioSoruce.PlayOneShot(_mainEngine);
            }
        }
        else
        {
            _audioSoruce.Stop();
            _mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        Vector3 force = Vector3.up * _thrustPower;
        _rigidBody.AddRelativeForce(force * Time.deltaTime);
       
    }

    private void RespondToRotateInput()
    {
        float rotationForFrame = _rotationPower * Time.deltaTime;
     

        _rigidBody.freezeRotation = true; // take manual control of the rotation
        if (Input.GetKey(KeyCode.A)) // cant rotate in both directions
        {
            // rotate left
            transform.Rotate(Vector3.forward * rotationForFrame); // Z axis
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rotate right
            transform.Rotate(Vector3.back * rotationForFrame); // Z axis
        }

        _rigidBody.freezeRotation = false; // resume physics control over rotation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_state != State.Alive) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":              
                break;
            case "Finish":
                OnLevelFinished();
                break;
            default:
                OnPlayerDeath();
                break;
        }
    }

    private void OnLevelFinished()
    {
        _state = State.Transcending;
        _successParticles.Play();
        _audioSoruce.PlayOneShot(_success);
            
        Invoke(nameof(LoadNextLevel), _levelLoadDelay);
    }

    private void OnPlayerDeath()
    {
        _state = State.Dying;
        _explosionParticles.Play();
        _audioSoruce.Stop();
        _audioSoruce.PlayOneShot(_explosion);
        Invoke(nameof(LoadFirstLevel), _levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        if (nextSceneIndex >= sceneCount)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}
