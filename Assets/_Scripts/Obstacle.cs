using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] bool m_randomizeHealth = true;
    [SerializeField] [ConditionalHide("m_randomizeHealth", Inverse = true)] float m_health = 100;
    [SerializeField] [ConditionalHide("m_randomizeHealth")] int m_minHealth = 10, m_maxHealth = 100;

    public int health { get { return (int)m_health; } }
    public int maxHealth { get { return m_maxHealth; } }    

    Runner m_runner;

    public Action onDamaged;
   


    TextMeshPro m_valueDisplay;

    int m_originalHealth;
    [SerializeField] float m_healthRewardMultiplier = 1.5f;
    [SerializeField] float m_shotRewardMultiplier = 0.5f;

    [SerializeField] float m_destructionPerSec = 1;

    [SerializeField] int m_bulletDmg = 5;

    [SerializeField] bool m_rewardPowerup;
    PowerUps m_powerUp;

    private void Awake()
    {
        m_runner = FindObjectOfType<Runner>();
        m_valueDisplay = GetComponentInChildren<TextMeshPro>();
        m_powerUp = m_runner.GetComponent<PowerUps>();

        if (m_randomizeHealth) m_health = UnityEngine.Random.Range(m_minHealth, m_maxHealth);
        UpdateValueDisplay();

        m_originalHealth = (int)m_health;
    }

    public void Damage(Collider _collider)
    {
        if (_collider.CompareTag("Player") )
        {
           m_health -= Time.fixedDeltaTime * m_destructionPerSec;
           UpdateValueDisplay();
            if (!m_powerUp.isInvulnerable)
            {
                m_runner.amount -= Time.fixedDeltaTime * m_destructionPerSec;
                m_runner.UpdateFollowers();
            }

         
            if (m_health <= 0)
            {
                m_runner.amount += (int)(m_originalHealth * m_healthRewardMultiplier);
                m_runner.UpdateValueDisplay();

                if (m_rewardPowerup) m_powerUp.BeginShoot();

                Destroy(gameObject);
            }

            onDamaged?.Invoke();
        }
    }
    public void BulletDamage(Collider _collider)
    {
        if (_collider.CompareTag("Pooletts"))
        {
            m_health -= m_bulletDmg;
            UpdateValueDisplay();

            if (m_health <= 0)
            {
                m_runner.amount += (int)(m_originalHealth * m_shotRewardMultiplier);
                m_runner.UpdateValueDisplay();

                Destroy(gameObject);
            }

            onDamaged?.Invoke();
        }
    }
    public void UpdateValueDisplay() => m_valueDisplay.text = health.ToString();


}
