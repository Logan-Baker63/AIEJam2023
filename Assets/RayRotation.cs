using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayRotation : MonoBehaviour
{
    [SerializeField] private Transform _rays;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _endValue;

    // Update is called once per frame
    //void Update()
    // {
    //     _rays.Rotate(0, 0, _rotateValue);
    // }

    private void Awake()
    {

    }
}
