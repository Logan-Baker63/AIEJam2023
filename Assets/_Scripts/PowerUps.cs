using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    public float powerUpDuration = 5f;
    [SerializeField] GameObject projectile;
    bool hasPowerUp = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            hasPowerUp = true;
            collision.gameObject.SetActive(false);
            Debug.Log("Touched");
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && hasPowerUp)
        {
            Debug.Log("Fire");
        }
    }
}
