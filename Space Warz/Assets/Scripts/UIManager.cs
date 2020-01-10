using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _score;

    [SerializeField]
    private Text _gameOver;

    [SerializeField]
    private Image _livesImage;

    [SerializeField]
    private Sprite[] _livesSprites;

    void Start()
    {
        this._score.text = "Score: 0";
    }

    public void UpdateScore(int score)
    {
        this._score.text = $"Score: {score}";
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
    }

    public void DisplayGameOver()
    {
        _gameOver.gameObject.SetActive(true);
    }


}
