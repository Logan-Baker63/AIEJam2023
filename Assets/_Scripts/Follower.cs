using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [HideInInspector] public Runner m_runner;

    Rigidbody rb;

    Vector3 m_followOffset;
    [SerializeField] float m_followDistance = -2;
    [SerializeField] float m_randRangeX = 2;
    
    [SerializeField] float m_speed = 15;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        float randNum = Random.Range(-m_randRangeX, m_randRangeX);
        m_followOffset = new Vector3(randNum, 0, m_followDistance);
    }

    private void Update()
    {
        transform.LookAt(m_runner.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        Vector3 dir = (m_runner.transform.position + m_followOffset - transform.position).normalized;

        Vector3 newPos = transform.position + (m_speed * Time.deltaTime * dir);
        if (newPos.z <= (m_runner.transform.position + m_followOffset).z)
        {
            transform.position = newPos;
        }
        else transform.position = m_runner.transform.position + m_followOffset;
    }
}
