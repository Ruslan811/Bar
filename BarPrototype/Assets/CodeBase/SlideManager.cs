using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SlideManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides; // Массив слайдов
    [SerializeField] private float slideDuration = 0.5f; // Длительность анимации
    [SerializeField] private Button[] slideButtons; // Массив кнопок для активации слайдов
    private int currentSlideIndex = 0; // Индекс текущего слайда

    private void Start()
    {
        ShowSlide(currentSlideIndex); // Показываем первый слайд

        // Привязываем кнопки к методу для активации слайдов по индексу
        for (int i = 0; i < slideButtons.Length; i++)
        {
            int index = i; // Сохраняем индекс кнопки
            slideButtons[i].onClick.AddListener(() => ShowSlide(index)); // Привязываем событие
        }
    }

    // Метод для перелистывания на указанный слайд
    public void ShowSlide(int index)
    {
        if (index >= 0 && index < slides.Length)
        {
            // Анимируем переход на слайд только если текущий слайд отличается от нового
            if (currentSlideIndex != index)
            {
                StartCoroutine(SlideTransition(slides[currentSlideIndex], slides[index]));
                currentSlideIndex = index; // Обновляем текущий индекс
            }
        }
    }

    // Корутин для анимации перелистывания слайдов
    private System.Collections.IEnumerator SlideTransition(GameObject oldSlide, GameObject newSlide)
    {
        // Если старый слайд существует, анимируем его исчезновение
        if (oldSlide != null)
        {
            oldSlide.transform.DOLocalMoveX(-Screen.width, slideDuration).OnKill(() => oldSlide.SetActive(false)); // Деактивируем старый слайд после анимации
        }

        // Активируем новый слайд
        newSlide.SetActive(true);
        newSlide.transform.localPosition = new Vector3(Screen.width, 0, 0); // Начальная позиция для анимации
        newSlide.transform.DOLocalMoveX(0, slideDuration); // Перемещаем слайд на его конечную позицию

        yield return new WaitForSeconds(slideDuration); // Ждем окончания анимации
    }
}
