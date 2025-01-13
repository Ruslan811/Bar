using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenWin : MonoBehaviour
{
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
        _window.SetActive(true);
        ApplyAnimation(_window.transform);
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
