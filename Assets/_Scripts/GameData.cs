using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] float m_speedIncreasePerSec = 1.67f;
    public float speedIncreasePerSec { get { return m_speedIncreasePerSec; } }
}
