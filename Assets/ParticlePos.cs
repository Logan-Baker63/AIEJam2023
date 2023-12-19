using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePos : MonoBehaviour
{
    [SerializeField] ParticleSystem _collisionParticles;
    private static Transform _particlePos;

    public static ParticlePos Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _collisionParticles = this.GetComponent<ParticleSystem>();

        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlayParticle(Transform pos)
    {
        _collisionParticles.transform.position = pos.position;
        _collisionParticles.Play();
        AudioManager.Instance.PlayGroupAudio("PoopShoot");
    }
}
