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
    [SerializeField] float powerUpDuration = 5f;
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
    }

    private void GainRegeneration()
    {
        throw new NotImplementedException();
    }

    private void GainExtraMultiplier()
    {
        throw new NotImplementedException();
    }

    private void GainShield()
    {
        throw new NotImplementedException();
    }
}
