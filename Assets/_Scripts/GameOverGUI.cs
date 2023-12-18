using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverGUI : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _winImage;
    [SerializeField] GameObject _failImage;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _hiScoreText;

    [Header("Debug")]
    [SerializeField] int _score;
    [SerializeField] int _hiScore;

    public void DisplayGameOverGUI()
    {
        Debug.Log("Allo there");

        _canvas.enabled = true;

        _scoreText.text = _score.ToString();
        _hiScoreText.text = _hiScore.ToString();

        if( _score > _hiScore )
        {
            _winImage.SetActive(true);
            _failImage.SetActive(false);
        }
        else if(_score <= _hiScore )
        {
            _winImage.SetActive(false);
            _failImage.SetActive(true);
        }
    }
}
