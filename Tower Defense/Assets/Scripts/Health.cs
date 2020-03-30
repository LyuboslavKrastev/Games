using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private ParticleSystem _hitParticlePrefab;
    [SerializeField] private ParticleSystem _deathParticlePrefab;
    [SerializeField] private AudioClip _hitSFX;
    [SerializeField] private AudioClip _deathSFX;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    void TakeDamage()
    {
        _health = Mathf.Max(_health - 1, 0);
        _hitParticlePrefab.Play();
        _audioSource.PlayOneShot(_hitSFX);

        if (_health == 0)
        {
            _audioSource.Stop();
            AudioSource.PlayClipAtPoint(_deathSFX, Camera.main.transform.position);
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
