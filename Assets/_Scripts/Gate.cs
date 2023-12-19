using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GateType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
} 

public class Gate : MonoBehaviour
{
    [SerializeField] GateType m_type;

    [SerializeField] bool m_randomizeAmount = true;
    [SerializeField] [ConditionalHide("m_randomizeAmount")] float m_minRange = 1, m_maxRange = 100;
    [SerializeField] [ConditionalHide("m_randomizeAmount", Inverse = true)] float m_changeAmount;

    Runner m_runner;

    TextMeshPro m_display;

    private void Awake()
    {
        m_runner = FindObjectOfType<Runner>();

        m_display = GetComponentInChildren<TextMeshPro>();
        UpdateDisplay();
    }

    private void OnValidate()
    {
        m_display = GetComponentInChildren<TextMeshPro>();
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        if (m_randomizeAmount) m_changeAmount = Random.Range(m_minRange, m_maxRange);

        if (m_type == GateType.Addition || m_type == GateType.Subtraction) m_changeAmount = (int)m_changeAmount;
        else if (m_type == GateType.Multiplication || m_type == GateType.Division) m_changeAmount = Mathf.Round(m_changeAmount * 10f) / 10f;

        if (m_type == GateType.Addition) m_display.text = "+";
        else if (m_type == GateType.Subtraction) m_display.text = "-";
        else if (m_type == GateType.Multiplication) m_display.text = "x";
        else if (m_type == GateType.Division) m_display.text = "/";

        m_display.text += m_changeAmount.ToString();
    }

    public void OnColliderEnter(Collider other)
    {
        CalculateNewRunnerAmount();
        AudioManager.Instance.PlaySFX("test");
    }

    void CalculateNewRunnerAmount()
    {
        float currAmt = m_runner.amount;

        if (m_type == GateType.Addition) currAmt += m_changeAmount;
        else if (m_type == GateType.Subtraction) currAmt -= m_changeAmount;
        else if (m_type == GateType.Multiplication) currAmt *= m_changeAmount;
        else if (m_type == GateType.Division) currAmt /= m_changeAmount;

        m_runner.amount = (int)currAmt;

        m_runner.UpdateFollowers();
    }
}
