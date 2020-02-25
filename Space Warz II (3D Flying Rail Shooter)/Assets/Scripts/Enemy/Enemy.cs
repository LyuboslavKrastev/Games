using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    private int pointsPerHit = 10;

    private int _hitsBeforeExploding = 3;


    private ScoreBoard _scoreBoard;
    void Start()
    {
        // Add the box collider at runtime so that the game does not break if the asset pack is reimported
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;

        _scoreBoard = FindObjectOfType<ScoreBoard>();

        if (_scoreBoard == null)
        {
            Debug.LogError("ScoreBoard is NULL");
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if (_hitsBeforeExploding < 1)
        {
            Die();
        }
        else
        {
            _hitsBeforeExploding -= 1;
        }
    }

    private void Die()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        _scoreBoard.UpdateScore(pointsPerHit);
        Destroy(gameObject);
    }
}
