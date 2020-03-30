using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    [SerializeField] private int _lives = 10;
    [SerializeField] private ParticleSystem _deathParticlePrefab;
    [SerializeField] private Text _livesText;
    [SerializeField] private AudioClip _baseDamageSFX;
    void Start()
    {
        UpdateLivesText();
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(_baseDamageSFX);
        TakeDamage();
    }
    private void UpdateLivesText()
    {
        _livesText.text = $"Lives: {_lives}";
    }

    void TakeDamage()
    {
        _lives = Mathf.Max(_lives - 1, 0);
        UpdateLivesText();

        if (_lives == 0)
        {
            Die();
        }
    }
    private void Die()
    {
        ParticleSystem deathFX = GameObject.Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        deathFX.Play();
        float destroyDelay = deathFX.main.duration;
        Destroy(deathFX.gameObject, destroyDelay);
        Destroy(this.gameObject);
    }
}
