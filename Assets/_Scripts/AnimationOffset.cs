using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    [SerializeField] float m_minOffset = 0, m_maxOffset = 1;

    Animator m_anim;
    float m_randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        m_randomOffset = Random.Range(m_minOffset, m_maxOffset);

        m_anim.Play("Run", 0, m_randomOffset);
    }
}
