using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_FiringHandler : MonoBehaviour
{
    private bool _firingEnabled = true;

    [SerializeField] private GameObject[] _guns;
    void Update()
    {
        if (_firingEnabled)
        {
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            Shoot();
        }
        else
        {
            StopShooting();
        }
    }

    void OnDeath() // called by string reference
    {
        _firingEnabled = false;

        StopShooting();
    }
    private void Shoot()
    {
        foreach (var gun in _guns)
        {
            gun.SetActive(true);
        }
    }

    private void StopShooting()
    {
        foreach (var gun in _guns)
        {
            gun.SetActive(false);
        }
    }
}
