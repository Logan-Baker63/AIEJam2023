using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera m_cam;
    [SerializeField] bool m_invert;

    private void Awake()
    {
        m_cam = Camera.main;
    }

    private void Update()
    {
        transform.forward = -m_cam.transform.forward;
        if (m_invert) transform.forward = -transform.forward;
    }
}
