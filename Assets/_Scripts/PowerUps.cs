using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class PowerUps : MonoBehaviour
{
    public float powerUpDuration = 5f;
    [SerializeField] List<GameObject> m_projectiles;
    [SerializeField] GameObject launchPoint;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float shootingInterval = 0.1f;
    bool hasPowerUp = false;
    public float invulnerabilityDuration = 5.0f;
    public bool isInvulnerable = false;
    Obstacle obstacle;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PowerUp")
        {
            collision.gameObject.SetActive(false);

            BeginShoot();
        }
        if (collision.gameObject.tag == "PooShields")
        {
            collision.gameObject.SetActive(false);

            GetInvulnerable();
        }
    }

    public void GetInvulnerable() => StartCoroutine(InvulnerabilityRoutine());

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        Debug.Log("can't touch me");
        isInvulnerable = false;
    }

    public void BeginShoot() => StartCoroutine(ShootForDuration());

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

    private void Shoot()
    {
        GameObject projectile = Instantiate(GetRandomProjectile(), transform.position + transform.forward, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * projectileSpeed;
        Destroy(projectile, 5);
    }



    GameObject GetRandomProjectile()
    {
        int randIndex = UnityEngine.Random.Range(0, m_projectiles.Count);
        return m_projectiles[randIndex];
    }
}
