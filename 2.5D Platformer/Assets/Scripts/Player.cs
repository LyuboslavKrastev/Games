using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private CharacterController _controller;

    private UIManager _uIManager;

    private float _speed = 5.0f;
    private float _gravity = 1.3f;
    private float _jumpStrength = 21.0f;

    private bool _canDoubleJump = false;

    private int _coins = 0;

    // this is a variable for caching our player's Y velocity, so it does not reset with each frame within the Update method and cause our jumps to not work as intended
    private float _yVelocity;

    private int _lives = 3;
    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is NULL!");
        }

        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uIManager == null)
        {
            Debug.LogError("UI manager is NULL!");
        }

        _uIManager.UpdateLivesText(_lives);
    }

    public void ReduceLives()
    {
        _lives -= 1;
        _uIManager.UpdateLivesText(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0); // reset the scene
        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            // may jump here
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpStrength;
                _canDoubleJump = true;
            }
        }
        else
        {
            // may double jump here
            if (Input.GetKeyDown(KeyCode.Space) && _canDoubleJump)
            {
                _yVelocity += _jumpStrength;
                _canDoubleJump = false;
            }
            _yVelocity -= _gravity;
        }

        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);      
    }

    public void IncreaseCoins()
    {
        _coins += 1;
        _uIManager.UpdateCoinsText(_coins);
    }
}
