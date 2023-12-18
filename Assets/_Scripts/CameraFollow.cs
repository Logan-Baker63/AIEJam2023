using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject m_target;

    float m_distZ;

    private void Awake()
    {
        m_distZ = m_target.transform.position.z - transform.position.z;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, m_target.transform.position.z - m_distZ);
    }
}
