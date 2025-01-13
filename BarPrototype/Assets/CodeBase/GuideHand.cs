using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GuideHand : MonoBehaviour
{
    [SerializeField] private GameObject handPrefab; // Префаб руки
    [SerializeField] private TMP_Text textPrefab; // Префаб текста
    [SerializeField] private Vector3 handOffset = new Vector3(50, -50, 0); // Смещение руки относительно кнопки
    [SerializeField] private Vector3 textOffset = new Vector3(0, 100, 0); // Смещение текста относительно кнопки
    [SerializeField] private float animationDuration = 1f; // Длительность анимации
    [SerializeField] private float animationDistance = 20f; // Расстояние движения руки
    [SerializeField] private string guideText = "Click here!"; // Текст гайда

    private GameObject handInstance;
    private GameObject textInstance;

    private const string FirstLaunchKey = "FirstLaunch"; // Ключ для хранения состояния первого запуска

    private void Start()
    {
        // Отключено сохранение состояния первого запуска для тестирования
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
        // Проверка, был ли уже выполнен первый запуск
        return !PlayerPrefs.HasKey(FirstLaunchKey);
    }

    private void SetFirstLaunchComplete()
    {
        // Устанавливаем флаг первого запуска
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

        // Создаем экземпляр руки
        handInstance = Instantiate(handPrefab, button.transform.parent);
        RectTransform handRect = handInstance.GetComponent<RectTransform>();
        RectTransform buttonRect = button.GetComponent<RectTransform>();

        if (handRect != null && buttonRect != null)
        {
            // Позиционируем руку относительно кнопки
            Vector3 buttonPosition = buttonRect.position;
            handRect.position = buttonPosition + handOffset;

            // Анимация руки
            AnimateHand(handRect);
        }
        else
        {
            Debug.LogError("GuideHand: RectTransform missing on handPrefab or button.");
        }

        // Создаем текст рядом с кнопкой
        textInstance = Instantiate(textPrefab.gameObject, button.transform.parent);
        RectTransform textRect = textInstance.GetComponent<RectTransform>();
        if (textRect != null && buttonRect != null)
        {
            Vector3 textPosition = CalculateTextPosition(buttonRect);
            textRect.position = textPosition;

            // Устанавливаем текстовое содержимое
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

        // Подписываемся на событие нажатия кнопки
        button.onClick.AddListener(() => OnButtonClicked(button));
    }

    private Vector3 CalculateTextPosition(RectTransform buttonRect)
    {
        // Вычисляем позицию текста за пределами кнопки
        Vector3 buttonSize = buttonRect.sizeDelta;
        Vector3 offset = new Vector3(0, buttonSize.y / 2 + textOffset.y, 0); // Смещение вверх за пределы кнопки
        return buttonRect.position + offset;
    }

    private void AnimateHand(RectTransform handRect)
    {
        // Анимация движения руки вверх-вниз
        Vector3 startPos = handRect.localPosition;
        Vector3 endPos = startPos + new Vector3(0, animationDistance, 0);

        handRect.DOLocalMove(endPos, animationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
    }

    private void OnButtonClicked(Button button)
    {
        // Удаляем экземпляры руки и текста
        if (handInstance != null)
        {
            Destroy(handInstance);
        }

        if (textInstance != null)
        {
            Destroy(textInstance);
        }

        // Убираем подписку с кнопки, чтобы избежать лишних вызовов
        button.onClick.RemoveListener(() => OnButtonClicked(button));
    }
}
