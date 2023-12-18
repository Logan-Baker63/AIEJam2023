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
    private float timer = 0f;
    [SerializeField] float powerUpDuration;
    [SerializeField] float regenRate;
    [SerializeField] float resilienceRate;
    [SerializeField] float numMultipler;
    bool hasShield;
    bool hasRegen;
    bool hasResilence;
    bool hasExtraMulti;
    bool isLooping;
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
            case PowerTypes.ExtraMultipler:
                {
                    hasRegen = true;
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
        //When entering negative (divide or subtraction) gate, lose less amount instead of full
        hasResilence = true;
    }

    private void GainRegeneration()
    {
       // Regains small amount of number for few seconds
    }

    private void GainExtraMultiplier()
    {
        //Gain extra numbers when entering the gate
        hasExtraMulti = true;
    }

}
