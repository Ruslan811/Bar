using UnityEngine;

public class FirstLaunchWindow : MonoBehaviour
{
    [SerializeField] private GameObject infoWindow; // Окно информации
    [SerializeField] private GameObject nextButton; // Кнопка "Next"
   // [SerializeField] private GameObject closeButton; // Кнопка "Close" (если нужна)

    private const string FirstLaunchKey = "FirstLaunch"; // Ключ для сохранения информации о первом запуске

    private void Start()
    {
        // Проверяем, был ли это первый запуск
        if (PlayerPrefs.GetInt(FirstLaunchKey, 0) == 0)
        {
            // Если это первый запуск, показываем окно
            infoWindow.SetActive(true);
        }
        else
        {
            // Если не первый запуск, скрываем окно сразу
            infoWindow.SetActive(false);
        }
    }

    // Метод для кнопки "Next"
    public void OnNextButtonClicked()
    {
        // Сохраняем, что первый запуск прошел
        PlayerPrefs.SetInt(FirstLaunchKey, 1);
        PlayerPrefs.Save();

        // Закрываем окно после того как пользователь нажал "Next"
        infoWindow.SetActive(false);
    }

    // Метод для кнопки "Close" (если нужно)
    public void OnCloseButtonClicked()
    {
        // Сохраняем, что первый запуск прошел
        PlayerPrefs.SetInt(FirstLaunchKey, 1);
        PlayerPrefs.Save();

        // Закрываем окно при нажатии на "Close"
        infoWindow.SetActive(false);
    }

    // Включение и отключение кнопок для переключения
    private void ToggleButtons(bool isVisible)
    {
        nextButton.SetActive(isVisible);
        //closeButton.SetActive(isVisible);
    }
}
