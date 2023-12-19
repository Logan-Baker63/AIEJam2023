using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Runner : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] InputActionReference m_move;
    [SerializeField] InputActionReference m_moveTouch;
    [SerializeField] float m_touchDistanceLeeway = 10;
    Camera cam;

    [SerializeField] float m_speed;
    [SerializeField] float m_sideSpeed;

    [SerializeField] float m_amount;
    public float amount { get { return m_amount; }
        set
        {
            m_amount = value;
            m_currentScore += value;
        }
    }

    public Transform m_followerParent;
    [SerializeField] GameObject m_followerPrefab;
    List<Follower> m_followers = new();

    TextMeshPro m_amountDisplay;

    [SerializeField] float m_moveLimit;

    bool m_isRunning = true;
    public bool isRunning { get { return m_isRunning; } set { m_isRunning = value; } }

    float m_currentScore;
    public float currentScore { get { return m_currentScore; } }

    GameData m_gameData;

    bool m_isDead;

    [SerializeField] float m_followerScaleIncreaseMulti = 0.025f;
    [SerializeField] List<Transform> m_followerPoints;

    [SerializeField] float m_maxFollowerScale = 20;

    PowerUps m_powerUps;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        m_amountDisplay = GetComponentInChildren<TextMeshPro>();
        m_gameData = FindObjectOfType<GameData>();
        m_powerUps = GetComponent<PowerUps>();
        cam = Camera.main;

        // Spawns followers
        for (int i = 0; i < m_followerPoints.Count; i++)
        {
            Follower follower = Instantiate(m_followerPrefab, m_followerPoints[i].position, Quaternion.identity).GetComponent<Follower>();
            follower.transform.SetParent(m_followerPoints[i]);
            m_followers.Add(follower);
            follower.m_runner = this;
        }

        UpdateValueDisplay();
    }

    private void FixedUpdate()
    {
        if (m_isRunning) rb.velocity = m_speed * Time.fixedDeltaTime * Vector3.forward;
        else rb.velocity = Vector3.zero;

        Move();
    }

    private void Update()
    {
        if (!m_powerUps.isInvulnerable)
        {
            m_speed += Time.deltaTime * m_gameData.speedIncreasePerSec;
        }
        else
        {
            m_speed = 2000 + (Time.deltaTime * m_gameData.speedIncreasePerSec);
            ResetSpeed();
        }

        //AudioManager.Instance.PlayGroupAudio("WalkingSFX2");

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            PlayerPrefs.SetInt("Highscore", 0);
        }
    }

    private void ResetSpeed() => StartCoroutine(ResetPlayerSpeed());

    private IEnumerator ResetPlayerSpeed()
    {
        yield return new WaitForSeconds(10);
        m_speed = 1000 + (Time.deltaTime * m_gameData.speedIncreasePerSec);
    }

    public void Move()
    {
        Vector2 dir = m_move.action.ReadValue<Vector2>();
        if (transform.position.x < m_moveLimit && dir.x > 0 ||
            transform.position.x > -m_moveLimit && dir.x < 0)
        {
            rb.velocity += Vector3.right * dir.x * m_sideSpeed * Time.fixedDeltaTime;
        }

        Vector2 touchScreenPos = m_moveTouch.action.ReadValue<Vector2>();
        if (touchScreenPos != Vector2.zero)
        {
            Vector2 runnerScreenPos = cam.WorldToScreenPoint(transform.position);

            if (touchScreenPos.x > runnerScreenPos.x + m_touchDistanceLeeway) // Run right 
            {
                if (transform.position.x < m_moveLimit) rb.velocity += m_sideSpeed * Time.fixedDeltaTime * Vector3.right;

            }
            else if (touchScreenPos.x < runnerScreenPos.x - m_touchDistanceLeeway)
            {
                if (transform.position.x > -m_moveLimit) rb.velocity += m_sideSpeed * Time.fixedDeltaTime * -Vector3.right;
            }
        }
    }

    public void UpdateFollowers()
    {
        if (m_amount <= 0)
        {
            // Die
            if (!m_isDead) Die();
        }
        else
        {
            int followerCap = m_followerPoints.Count;
            float scaleMulti = ((int)(m_amount / followerCap) * m_followerScaleIncreaseMulti) + 1;

            foreach (Follower follower in m_followers)
            {
                Vector3 newScale = Vector3.one * scaleMulti;
                if (newScale.x > m_maxFollowerScale) follower.transform.localScale = Vector3.one * m_maxFollowerScale;
                else follower.transform.localScale = newScale;
            }
        }

        UpdateValueDisplay();
    }

    void Die()
    {
        Time.timeScale = 0;

        // Update highscore
        if (PlayerPrefs.GetInt("Highscore") < (int)m_currentScore)
        {
            // New highscore!
            PlayerPrefs.SetInt("Highscore", (int)m_currentScore);
        }

        int highScore = PlayerPrefs.GetInt("Highscore");
        // Show UI
        GameOverGUI.Instance.DisplayGameOverGUI((int)m_currentScore, highScore);
    }

    public void UpdateValueDisplay() => m_amountDisplay.text = ((int)m_amount).ToString();
}
