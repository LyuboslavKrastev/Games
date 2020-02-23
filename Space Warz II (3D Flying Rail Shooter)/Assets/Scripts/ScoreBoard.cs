using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    private int _score;
    private Text _scoreText;
    private int pointsPerHit = 10;
    void Start()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = $"Score: {_score}";
    }
    public void UpdateScore()
    {
        _score += pointsPerHit;
        _scoreText.text = $"Score: {_score}";
    }
}
