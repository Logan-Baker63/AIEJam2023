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
    bool hasShield;
    bool hasRegen;
    bool hasResilence;
    bool hasExtraMulti;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GainPowerUp(other.gameObject);
            gameObject.SetActive(false);
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
        throw new NotImplementedException();
        hasResilence = true;
    }

    private void GainRegeneration()
    {
        throw new NotImplementedException();
        hasRegen = true;
    }

    private void GainExtraMultiplier()
    {
        throw new NotImplementedException();
        hasExtraMulti = true;
    }

    private void GainShield()
    {
        throw new NotImplementedException();
        hasShield = true;
    }
}
