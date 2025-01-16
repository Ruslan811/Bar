using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GuideHand : MonoBehaviour
{
    [SerializeField] private GameObject handPrefab;
    [SerializeField] private Vector3 handOffset = new Vector3(0, 0, 0);
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private float animationDistance = 20f;
    [SerializeField] private string guideText = "Click here!";

    private GameObject handInstance;
    private GameObject textInstance;

    private void Start()
    {
        ShowGuide();
    }

    private void ShowGuide()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            SpawnHandAndText(button);
        }
        else
        {
            Debug.LogError("GuideHand: Button component not found on the GameObject.");
        }
    }

    private void SpawnHandAndText(Button button)
    {
       

        // ������� ��������� ���� ��� �������� ������ ������
        handInstance = Instantiate(handPrefab, button.transform);
        RectTransform handRect = handInstance.GetComponent<RectTransform>();
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        if (handRect != null && buttonRect != null)
        {
            handRect.localPosition = handOffset; // ��������� ������� ������������ ������ ������
            AnimateHand(handRect);
        }
        else
        {
            Debug.LogError("GuideHand: RectTransform missing on handPrefab or button.");
        }

      

        button.onClick.AddListener(() => OnButtonClicked(button));
    }

    private void AnimateHand(RectTransform handRect)
    {
        Vector3 startPos = handRect.localPosition;
        Vector3 endPos = startPos + new Vector3(0, animationDistance, 0);

        handRect.DOLocalMove(endPos, animationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
    }

    private void OnButtonClicked(Button button)
    {
        if (handInstance != null)
        {
            Destroy(handInstance);
        }

        if (textInstance != null)
        {
            Destroy(textInstance);
        }

        button.onClick.RemoveListener(() => OnButtonClicked(button));
    }
}