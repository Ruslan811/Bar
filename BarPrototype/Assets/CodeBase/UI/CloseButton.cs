using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CloseButton : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 offScreenPosition;
    [SerializeField] private GameObject _window;

    private Button _buttton;

    private void Start()
    {
        _buttton = GetComponent<Button>();
        _buttton.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        _window.transform.DOMove(offScreenPosition, animationDuration)
            .SetEase(Ease.InBack)
            .OnComplete(() => _window.SetActive(false));
    }
}