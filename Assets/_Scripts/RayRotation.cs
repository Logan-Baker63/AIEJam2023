using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayRotation : MonoBehaviour
{
    [SerializeField] private Transform _rays;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _rotValue;

    private void Awake()
    {
        Vector3 byValue = new Vector3(0, 0, _rotValue);
        _rays.transform.DOBlendableRotateBy(byValue, _rotateSpeed).SetLoops(-1, LoopType.Yoyo);
    }
}
