using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class GameOverGUI : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _winImage;
    [SerializeField] GameObject _failImage;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highScoreText;

    [Header("Debug")]
    [SerializeField] int _score;
    [SerializeField] int _highScore;
    public static GameOverGUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplayGameOverGUI(int currentScore, int highScore)
    {
        Debug.Log("Allo there");

        _canvas.enabled = true;

        _scoreText.text = currentScore.ToString();
        _highScoreText.text = highScore.ToString();

        if(currentScore > highScore)
        {
            _winImage.SetActive(true);
            _failImage.SetActive(false);
        }
        else if(currentScore <= highScore)
        {
            _winImage.SetActive(false);
            _failImage.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Debug.Log("Reloadedededed");
        SceneManager.LoadScene(1);
    }
}
