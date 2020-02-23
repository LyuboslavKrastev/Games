using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    void Start()
    {
        // Add the box collider at runtime so that the game does not break if the asset pack is reimported
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
