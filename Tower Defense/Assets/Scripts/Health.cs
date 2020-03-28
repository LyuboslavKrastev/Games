using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 10;
    private void OnParticleCollision(GameObject other)
    {
        TakeDamage();
    }

    void TakeDamage()
    {
        _health = Mathf.Max(_health - 1, 0);
        if (_health == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}
