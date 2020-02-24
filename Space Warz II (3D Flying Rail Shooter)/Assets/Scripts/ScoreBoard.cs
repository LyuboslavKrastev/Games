using UnityEngine;
using UnityEngine.UI;
public class ScoreBoard : MonoBehaviour
{
    private int _score;
    private Text _scoreText;
    void Start()
    {
        _scoreText = GetComponent<Text>();
        _scoreText.text = $"Score: {_score}";
    }
    public void UpdateScore(int points)
    {
        _score += points;
        _scoreText.text = $"Score: {_score}";
    }
}
