using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameOverGUI : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _winImage;
    [SerializeField] GameObject _failImage;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _highScoreText;
    GameOverCanvas _gameOverCanvas;

    [Header("Debug")]
    public static GameOverGUI Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Init();

            SceneManager.activeSceneChanged += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        _gameOverCanvas = FindObjectOfType<GameOverCanvas>();
        _canvas = _gameOverCanvas.GetComponent<Canvas>();
        _winImage = _gameOverCanvas._winImage;
        _failImage = _gameOverCanvas._failImage;
        _scoreText = _gameOverCanvas._scoreText;
        _highScoreText = _gameOverCanvas._highScoreText;
    }

    void OnSceneLoaded(Scene _scene, Scene _scene2)
    {
        Init();
    }

    public void DisplayGameOverGUI(int currentScore, int highScore)
    {
        _canvas.enabled = true;

        _scoreText.text = currentScore.ToString();
        _highScoreText.text = highScore.ToString();

        if(currentScore >= highScore)
        {
            _winImage.SetActive(true);
            _failImage.SetActive(false);
        }
        else if(currentScore < highScore)
        {
            _winImage.SetActive(false);
            _failImage.SetActive(true);
        }
    }

    public static void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
