using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _endValue;
    [SerializeField] private float _duration;


    // Update is called once per frame
    void Awake()
    {
        _transform.DOScale(_endValue, _duration).SetLoops(-1, LoopType.Yoyo);
    }

   


}
