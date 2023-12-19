using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [HideInInspector] public Runner m_runner;

    private void Update()
    {
        transform.LookAt(m_runner.transform);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        transform.localPosition = Vector3.zero;
    }
}
