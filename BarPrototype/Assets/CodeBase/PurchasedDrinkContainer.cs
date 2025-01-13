using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PurchasedDrinkContainer : MonoBehaviour
{
    [SerializeField] private Transform contentParent; // Родитель для слотов
    [SerializeField] private DrinkSlot slotPrefab; // Префаб слота
    [SerializeField] private float spacing = 25f; // Отступ между слотами
    [SerializeField] private RectTransform containerRect; // Контейнер
    [SerializeField] private ScrollRect scrollRect; // ScrollRect для прокрутки
    [SerializeField] private ShopManager _manager; // Менеджер магазина

    private void OnEnable()
    {
        SpawnPurchasedDrinks();
        AdjustScrollRect();
    }

    private void SpawnPurchasedDrinks()
    {
        float currentYPosition = 0f;

        // Получаем высоту контейнера для сравнения
        float containerHeight = containerRect.rect.height;

        // Удаляем предыдущие слоты
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Получаем все купленные напитки
        foreach (var drinkEntry in _manager.GetPurchasedDrinks())
        {
            var drink = drinkEntry.Value.slot;
            int quantity = drinkEntry.Value.quantity;

            // Пропускаем напитки, которые были удалены
            if (_manager.IsDrinkRemoved(drink.DrinkName))
            {
                continue;
            }

            // Создаем новый слот для напитка
            DrinkSlot newSlot = Instantiate(slotPrefab, contentParent);
            newSlot.Setup(drink);
            newSlot.SetQuantity(quantity);
            newSlot.EnableRemoveButton();

            // Устанавливаем позицию слота
            RectTransform slotRect = newSlot.GetComponent<RectTransform>();
            slotRect.anchoredPosition = new Vector2(0, currentYPosition);

            currentYPosition -= slotRect.rect.height + spacing;
        }

        // Подгоняем размер контента под общее количество слотов
        float contentHeight = Mathf.Max(-currentYPosition, containerHeight);
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, contentHeight);
    }

    private void AdjustScrollRect()
    {
        // Убедимся, что ScrollRect настроен правильно
        if (scrollRect != null && containerRect != null)
        {
            scrollRect.content = containerRect;
            scrollRect.vertical = true; // Включаем вертикальную прокрутку
            scrollRect.horizontal = false; // Отключаем горизонтальную прокрутку
        }
    }

    public void UpdateContainer()
    {
        SpawnPurchasedDrinks();
        AdjustScrollRect();
    } 

    public void AnimateAndRemoveDrink(DrinkSlot drinkSlot)
    {
        // Для анимации использования и удаления напитков
        drinkSlot.transform.DOMove(contentParent.position, 0.3f).OnComplete(() =>
        {
            Destroy(drinkSlot.gameObject); // Удалить слот
            _manager.RemovePurchasedDrink(drinkSlot.DrinkName); // Удалить напиток из менеджера
           // UpdateContainer(); // Обновить контейнер
        });
    }
}
