using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnim : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 offScreenPosition;

    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = transform.position;
        transform.position = offScreenPosition;
    }

    private void OnEnable()
    {
        AnimateWindowIn();
    }

    private void AnimateWindowIn()
    {
        transform.DOMove(_targetPosition, animationDuration).SetEase(Ease.OutBack);
    }
}
