using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    [SerializeField] private ParticleSystem _hitParticlePrefab;
    [SerializeField] private ParticleSystem _deathParticlePrefab;

    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    void TakeDamage()
    {
        _health = Mathf.Max(_health - 1, 0);
        _hitParticlePrefab.Play();

        if (_health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        ParticleSystem deathFX = GameObject.Instantiate(_deathParticlePrefab, transform.position, Quaternion.identity);
        deathFX.Play();
        float destroyDelay = deathFX.main.duration;
        Destroy(deathFX.gameObject, destroyDelay);
        Destroy(this.gameObject);
    }
}
