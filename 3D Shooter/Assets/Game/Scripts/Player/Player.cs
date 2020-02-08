using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{ 
    CharacterController _controller;

    private float _speed = 3;
    private float _gravity = 1.2f;
    private float _jumpStrength = 16.0f;
    private float yVelocity = 0;

    [SerializeField]
    private GameObject _muzzleFlash;


    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("CharacterController is NULL!");
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _muzzleFlash.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _muzzleFlash.SetActive(true);
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
        }
        CalculateMovment();
    }

    private static void Shoot()
    {       
        Vector3 centerPosition = new Vector3(0.5f, 0.5f, 0);

        Ray rayOrigin = Camera.main.ViewportPointToRay(centerPosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo))
        {
            Debug.Log($"You hit {hitInfo.transform.name}");
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
