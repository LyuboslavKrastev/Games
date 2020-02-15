using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private AudioSource _audioSoruce;

    [SerializeField]
    private float _rotationPower = 100f;

    [SerializeField]
    private float _thrustPower = 20f;
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
        Thrust();
        Rotate();
    }
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            Vector3 force = Vector3.up * _thrustPower;
            _rigidBody.AddRelativeForce(force);
            if (!_audioSoruce.isPlaying) // so it does not layer
            {
                _audioSoruce.Play();
            }
        }
        else
        {
            _audioSoruce.Stop();
        }
    }
    private void Rotate()
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
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }
}
