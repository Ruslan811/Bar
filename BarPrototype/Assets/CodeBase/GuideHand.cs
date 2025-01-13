using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GuideHand : MonoBehaviour
{
    [SerializeField] private GameObject handPrefab; // ������ ����
    [SerializeField] private TMP_Text textPrefab; // ������ ������
    [SerializeField] private Vector3 handOffset = new Vector3(50, -50, 0); // �������� ���� ������������ ������
    [SerializeField] private Vector3 textOffset = new Vector3(0, 100, 0); // �������� ������ ������������ ������
    [SerializeField] private float animationDuration = 1f; // ������������ ��������
    [SerializeField] private float animationDistance = 20f; // ���������� �������� ����
    [SerializeField] private string guideText = "Click here!"; // ����� �����

    private GameObject handInstance;
    private GameObject textInstance;

    private const string FirstLaunchKey = "FirstLaunch"; // ���� ��� �������� ��������� ������� �������

    private void Start()
    {
        // ��������� ���������� ��������� ������� ������� ��� ������������
        /*
        if (IsFirstLaunch())
        {
            ShowGuide();
            SetFirstLaunchComplete();
        }
        */
        ShowGuide();
    }

    private bool IsFirstLaunch()
    {
        // ��������, ��� �� ��� �������� ������ ������
        return !PlayerPrefs.HasKey(FirstLaunchKey);
    }

    private void SetFirstLaunchComplete()
    {
        // ������������� ���� ������� �������
        PlayerPrefs.SetInt(FirstLaunchKey, 1);
        PlayerPrefs.Save();
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
        if (handPrefab == null || textPrefab == null)
        {
            Debug.LogError("GuideHand: Hand or text prefab is not assigned.");
            return;
        }

        // ������� ��������� ����
        handInstance = Instantiate(handPrefab, button.transform.parent);
        RectTransform handRect = handInstance.GetComponent<RectTransform>();
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        if (handRect != null && buttonRect != null)
        {
            // ������������� ���� ������������ ������
            Vector3 buttonPosition = buttonRect.position;
            handRect.position = buttonPosition + handOffset;

            // �������� ����
            AnimateHand(handRect);
        }
        else
        {
            Debug.LogError("GuideHand: RectTransform missing on handPrefab or button.");
        }

        // ������� ����� ����� � �������
        textInstance = Instantiate(textPrefab.gameObject, button.transform.parent);
        RectTransform textRect = textInstance.GetComponent<RectTransform>();
        if (textRect != null && buttonRect != null)
        {
            Vector3 textPosition = CalculateTextPosition(buttonRect);
            textRect.position = textPosition;

            // ������������� ��������� ����������
            TextMeshProUGUI textComponent = textInstance.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = guideText;
            }
        }
        else
        {
            Debug.LogError("GuideHand: RectTransform missing on textPrefab.");
        }

        // ������������� �� ������� ������� ������
        button.onClick.AddListener(() => OnButtonClicked(button));
    }

    private Vector3 CalculateTextPosition(RectTransform buttonRect)
    {
        // ��������� ������� ������ �� ��������� ������
        Vector3 buttonSize = buttonRect.sizeDelta;
        Vector3 offset = new Vector3(0, buttonSize.y / 2 + textOffset.y, 0); // �������� ����� �� ������� ������
        return buttonRect.position + offset;
    }

    private void AnimateHand(RectTransform handRect)
    {
        // �������� �������� ���� �����-����
        Vector3 startPos = handRect.localPosition;
        Vector3 endPos = startPos + new Vector3(0, animationDistance, 0);

        handRect.DOLocalMove(endPos, animationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
    }

    private void OnButtonClicked(Button button)
    {
        // ������� ���������� ���� � ������
        if (handInstance != null)
        {
            Destroy(handInstance);
        }

        if (textInstance != null)
        {
            Destroy(textInstance);
        }

        // ������� �������� � ������, ����� �������� ������ �������
        button.onClick.RemoveListener(() => OnButtonClicked(button));
    }
}
