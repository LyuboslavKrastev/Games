using UnityEngine;
using UnityEngine.SceneManagement; // ok only if this is the only script loading screens

public class Player_CollisionHandler : MonoBehaviour
{
    [Tooltip("In seconds")][SerializeField] private float _levelLoadDelay = 2f;
    [Tooltip("Particle effects prefab on player")][SerializeField] private GameObject _explosion;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        _explosion.SetActive(true);
        Invoke(nameof(ReloadLevel), _levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        // Actor model
        SendMessage("OnDeath"); // calling the method within the movement handler
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(1);
    }
}
