using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum DisplayType
{
    Value,
    Score,
    Highscore
}

public class Display : MonoBehaviour
{
    Runner m_runner;
    TextMeshProUGUI m_display;

    [SerializeField] DisplayType m_type;
    [SerializeField] string m_displayText;

    private void Awake()
    {
        m_runner = FindObjectOfType<Runner>();
        m_display = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        m_display.text = m_displayText + "\n";
        if (m_type == DisplayType.Value) m_display.text += m_runner.amount;
        else if (m_type == DisplayType.Score) m_display.text += m_runner.currentScore;
        else if (m_type == DisplayType.Highscore) m_display.text += PlayerPrefs.GetInt("Highscore");
    }
}
