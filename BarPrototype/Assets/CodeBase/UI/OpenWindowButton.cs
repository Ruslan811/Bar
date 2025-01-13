using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
public partial class OpenWindowButton : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _window;
    [SerializeField] private AnimationType _animationType; // Тип анимации

    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Open);
    }

    private void Open()
    {
        GameObject instantiatedWindow = Instantiate(_window, _container.position, Quaternion.identity, _container.parent);
        ApplyAnimation(instantiatedWindow.transform);
    }

    private void ApplyAnimation(Transform windowTransform)
    {
        switch (_animationType)
        {
            case AnimationType.Scale:
                windowTransform.localScale = Vector3.zero;
                windowTransform.DOScale(Vector3.one, 0.5f);
                break;

            case AnimationType.FadeIn:
                CanvasGroup canvasGroup = windowTransform.gameObject.AddComponent<CanvasGroup>();
                canvasGroup.alpha = 0;
                canvasGroup.DOFade(1, 0.5f);
                break;

            case AnimationType.SlideFromLeft:
                Vector3 startPos = windowTransform.position;
                windowTransform.position = new Vector3(-Screen.width, startPos.y, startPos.z);
                windowTransform.DOMoveX(startPos.x, 0.5f);
                break;

            case AnimationType.SlideFromRight:
                Vector3 startPosRight = windowTransform.position;
                windowTransform.position = new Vector3(Screen.width, startPosRight.y, startPosRight.z);
                windowTransform.DOMoveX(startPosRight.x, 0.5f);
                break;

            default:
                Debug.LogWarning("Unknown animation type.");
                break;
        }
    }
}
