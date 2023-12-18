using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GateType
{
    Addition,
    Subtraction,
    Multiplication,
    Division
} 

public class Gate : MonoBehaviour
{
    [SerializeField] GateType m_type;

    [SerializeField] float m_changeAmount;
}
