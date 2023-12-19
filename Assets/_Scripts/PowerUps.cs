using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    public float powerUpDuration = 5f;
    [SerializeField] GameObject projectilePrefab, launchPoint;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shootingInterval = 0.1f;
    bool hasPowerUp = false;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {

            collision.gameObject.SetActive(false);

            //TODO: Shoot() repeatly until powerupDuration rans outs
            StartCoroutine(ShootForDuration());
            Debug.Log("Touched");
        }
    }




    private IEnumerator ShootForDuration()
    {
        hasPowerUp = true;
        float endTime = Time.time + powerUpDuration;

        while (Time.time < endTime)
        {
            Shoot();
            yield return new WaitForSeconds(shootingInterval);
        }

        hasPowerUp = false;
    }
    //private void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.Space) && hasPowerUp)
    //    {
    //        Shoot();
    //        //Debug.Log("Fire");
    //    }
    //}

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        Destroy(projectile, 5);
    }
}
