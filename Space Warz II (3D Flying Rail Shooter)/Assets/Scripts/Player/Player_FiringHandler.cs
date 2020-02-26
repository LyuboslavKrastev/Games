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
            IsShooting(true);
        }
        else
        {
            IsShooting(false);
        }
    }

    void OnDeath() // called by string reference
    {
        _firingEnabled = false;
    }
    private void IsShooting(bool isShooting)
    {
        foreach (var gun in _guns)
        {
            var emission = gun.GetComponent<ParticleSystem>().emission;
            emission.enabled = isShooting;
        }
    }
}
