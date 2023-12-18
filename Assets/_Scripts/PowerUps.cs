using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerTypes
{
    Shield,
    ExtraMultipler,
    Regeneration,
    Resilience
}

public class PowerUps : MonoBehaviour
{
    public PowerTypes powerType;
    [SerializeField] float powerUpDuration;
    [SerializeField] float regenRate;
    [SerializeField] float resilienceRate;
    [SerializeField] float numMultipler;
    [SerializeField] GameObject playerShield;
    bool hasShield;
    bool hasRegen;
    bool hasResilence;
    bool hasExtraMulti;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameController")
        {
            GainPowerUp(other.gameObject);
            gameObject.SetActive(false);
            Debug.Log("touched");
        }
    }

    private void GainPowerUp(GameObject gameObject)
    {
        switch (powerType)
        {
            case PowerTypes.Shield:
                {
                    GainShield();
                    break;
                }
            case PowerTypes.ExtraMultipler:
                {
                    GainExtraMultiplier();
                    break;
                }
            case PowerTypes.Regeneration:
                {
                    GainRegeneration();
                    break;
                }
            case PowerTypes.Resilience:
                {
                    GainResilence();
                    break;
                }
        }
    }

    private void GainResilence()
    {
        hasResilence = true;
    }

    private void GainRegeneration()
    {
        hasRegen = true;
    }

    private void GainExtraMultiplier()
    {
        hasExtraMulti = true;
    }

    private void GainShield()
    {
        hasShield = true;
        playerShield.SetActive(true);
    }
}
