using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Runner : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] InputActionReference m_move;

    [SerializeField] float m_speed;
    [SerializeField] float m_sideSpeed;

    [SerializeField] int m_amount;
    public int amount { get { return m_amount; } set { m_amount = value; } }

    [SerializeField] Transform m_followerParent;
    [SerializeField] GameObject m_followerPrefab;
    List<Follower> m_followers = new();

    TextMeshPro m_amountDisplay;

    [SerializeField] float m_moveLimit;

    private void Awake()
    {
        rb = FindObjectOfType<Rigidbody>();
        m_amountDisplay = GetComponentInChildren<TextMeshPro>();

        m_amountDisplay.text = m_amount.ToString();
    }

    private void FixedUpdate()
    {
        rb.velocity = m_speed * Time.fixedDeltaTime * Vector3.forward;
        Move();
    }

    public void Move()
    {
        Vector2 dir = m_move.action.ReadValue<Vector2>();
        if (transform.position.x < m_moveLimit && dir.x > 0 ||
            transform.position.x > -m_moveLimit && dir.x < 0)
        {
            rb.velocity += Vector3.right * dir.x * m_sideSpeed * Time.fixedDeltaTime;
        }
        
    }

    public void UpdateFollowers()
    {
        if (m_followers.Count > m_amount + 1)
        {
            int iterations = m_followers.Count - m_amount + 1;
            for (int i = 0; i < iterations; i++)
            {
                Destroy(m_followers[0].gameObject);
                m_followers.RemoveAt(0);
            }
        }
        else if (m_followers.Count < m_amount + 1)
        {
            int iterations = m_amount + 1 - m_followers.Count;
            for (int i = 0; i < iterations; i++)
            {
                if (m_followers.Count < 15)
                {
                    Follower follower = Instantiate(m_followerPrefab, transform.position, Quaternion.identity).GetComponent<Follower>();
                    follower.transform.SetParent(m_followerParent);
                    m_followers.Add(follower);
                    follower.m_runner = this;
                }
            }
        }

        m_amountDisplay.text = m_amount.ToString();
    }
}
