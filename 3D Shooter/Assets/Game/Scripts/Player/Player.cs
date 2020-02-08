using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{ 
    private CharacterController _controller;

    private UIManager _UIManager;

    [SerializeField]
    private Transform _reloadTransform;
    Vector3 _weaponStartPosition;
    Quaternion _weaponStartRotation;

    private float _speed = 3;
    private float _gravity = 1.2f;
    private float _jumpStrength = 16.0f;
    private float yVelocity = 0;

    private int _currentAmmo;
    private int _maxAmmo = 150;

    private bool _isReloading = false;
    private bool _hasWeapon = false;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitMarkerPrefab;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioSource _reloadAudioSource;

    [SerializeField]
    private GameObject _weapon;

    private int _coins = 0;

    void Start()
    {
        _weapon.SetActive(false);
        _weaponStartPosition = _weapon.transform.localPosition;
        _weaponStartRotation = _weapon.transform.localRotation;
        _controller = GetComponent<CharacterController>();

        _reloadAudioSource = GetComponent<AudioSource>();

        if (_reloadAudioSource == null)
        {
            Debug.Log("AudioSource(Reload) is NULL!");
        }

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

    public int GetCoins()
    {
        return _coins;
    }
    public void BuyWeapon()
    {
        _coins -= 1;
        _weapon.SetActive(true);
        _UIManager.HideCoin();
        _hasWeapon = true;
    }

    public void TakeCoin()
    {
        _coins += 1;
        _UIManager.ShowCoin();
    }
    IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        _UIManager.HideReloadWarning();
        _UIManager.ShowReloading();
        _reloadAudioSource.Play();

        // Simulate reloading by moving and turn the weapon
        _weapon.transform.position = _reloadTransform.position;
        _weapon.transform.rotation = _reloadTransform.rotation;

        yield return new WaitForSeconds(1.0f);
        _reloadAudioSource.Stop();
        _isReloading = false;
        _currentAmmo = _maxAmmo;
        _UIManager.HideReloading();
        _UIManager.UpdateAmmo(_currentAmmo);
        
        // Get the weapon to the original position
        _weapon.transform.localPosition = _weaponStartPosition;
        _weapon.transform.localRotation = _weaponStartRotation;
    }
    void Update()
    {
        UseWeapon();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovment();
    }

    private void UseWeapon()
    {
        if (_hasWeapon)
        {
            if (Input.GetKeyDown(KeyCode.R) && _isReloading == false)
            {
                StartCoroutine(ReloadRoutine());
            }
            if (Input.GetMouseButton(0) && _currentAmmo > 0 && _isReloading == false)
            {
                Shoot();
            }
            else
            {
                if (_currentAmmo <= 0 && !_isReloading)
                {
                    _UIManager.ShowReloadWarning();
                }
                _muzzleFlash.SetActive(false);
                _audioSource.Stop();
            }
        }
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

            // check if hit a crate
            // destroy crate

            Destructable crate = hitInfo.transform.GetComponent<Destructable>();

            if (crate != null)
            {
                crate.DestroyCrate();
            }
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
