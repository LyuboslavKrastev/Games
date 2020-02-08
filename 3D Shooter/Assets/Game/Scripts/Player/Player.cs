using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    private CharacterController _controller;

    private UIManager _UIManager;

    private float _speed = 3;
    private float _gravity = 1.2f;
    private float _jumpStrength = 16.0f;
    private float yVelocity = 0;

    private int _currentAmmo;
    private int _maxAmmo = 150;

    bool reloading = false;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is NULL!");
        }

        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_UIManager == null)
        {
            Debug.LogError("UIManager is NULL!");
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _muzzleFlash.SetActive(false);

        _currentAmmo = _maxAmmo;
        _UIManager.UpdateAmmo(_currentAmmo);
    }

    IEnumerator ReloadRoutine()
    {
        reloading = true;
        _UIManager.HideReloadWarning();
        _UIManager.ShowReloading();
        yield return new WaitForSeconds(1.0f);
        _currentAmmo = _maxAmmo;
        _UIManager.HideReloading();
        _UIManager.UpdateAmmo(_currentAmmo);
        reloading = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && reloading == false)
        {
            StartCoroutine(ReloadRoutine());
        }
        if (Input.GetMouseButton(0) && _currentAmmo > 0 && reloading == false)
        {
            Shoot();  
        }
        else
        {
            if (_currentAmmo <= 0 && !reloading)
            {
                _UIManager.ShowReloadWarning();
            }
            _muzzleFlash.SetActive(false);
            _audioSource.Stop();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovment();
    }

    private void Shoot()
    {
        _muzzleFlash.SetActive(true);
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        _currentAmmo -= 1;
        _UIManager.UpdateAmmo(_currentAmmo);
        Vector3 centerPosition = new Vector3(0.5f, 0.5f, 0);

        Ray rayOrigin = Camera.main.ViewportPointToRay(centerPosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log($"You hit {hitInfo.transform.name}");

            var lookRotation = Quaternion.LookRotation(hitInfo.normal);

            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, lookRotation);

            Destroy(hitMarker, 0.5f);
        }
    }

    private void CalculateMovment()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;

        if (Input.GetKeyDown(KeyCode.Space) && _controller.isGrounded)
        {
            yVelocity = _jumpStrength;
            NavMeshAgent navMeshAgent = GetComponent<NavMeshAgent>();
            StartCoroutine(EnableNavMeshAgent(navMeshAgent));
            navMeshAgent.enabled = false;
        }
        

        yVelocity -= _gravity;

        velocity.y = yVelocity; // applying gravity here

        // asign world space values to the velocity so we move based on world space values
        velocity = transform.TransformDirection(velocity); 
        _controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator EnableNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        yield return new WaitForSeconds(0.5f);
        navMeshAgent.enabled = true;
    }
}
