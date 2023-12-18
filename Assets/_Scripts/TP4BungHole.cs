using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP4BungHole : MonoBehaviour
{
    [SerializeField] GameObject _tp1;
    [SerializeField] GameObject _tp2;
    [SerializeField] GameObject _tp3;
    [SerializeField] GameObject _tp4;

    Obstacle m_obstacle;

    private void Awake()
    {
        m_obstacle = GetComponentInParent<Obstacle>();
        m_obstacle.onDamaged += UpdateRollSize;
    }
    public void UpdateRollSize()
    {
        int currentHealth = m_obstacle.health;
        int maxHealth = m_obstacle.maxHealth;

        if(currentHealth > (maxHealth * 0.75))
        {
            SetAllToFalse();
            _tp1.SetActive(true);
        }
        if (currentHealth > (maxHealth * 0.50) && currentHealth < (maxHealth * 0.75))
        {
            SetAllToFalse();
            _tp2.SetActive(true);
        }
        if (currentHealth < (maxHealth * 0.50) && currentHealth > (maxHealth * 0.25))
        {
            SetAllToFalse();
            _tp3.SetActive(true);
        }
        if (currentHealth < (maxHealth * 0.25))
        {
            SetAllToFalse();
            _tp4.SetActive(true);
        }
    }

    public void SetAllToFalse()
    {
        _tp1.SetActive(false);
        _tp2.SetActive(false);
        _tp3.SetActive(false);
        _tp4.SetActive(false);
    }
}
