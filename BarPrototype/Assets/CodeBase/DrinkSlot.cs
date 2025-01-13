using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DrinkSlot : MonoBehaviour
{
    [SerializeField] private Image drinkImage;
    [SerializeField] private Text drinkNameText;
    [SerializeField] private Text drinkPriceText;
    [SerializeField] private Text drinkQuantityText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button removeButton; // Кнопка удаления
    [SerializeField] private Sprite sprite;
    [SerializeField] private string drinkName;
    [SerializeField] private uint price;

    [SerializeField] private Transform purchaseTarget;
    [SerializeField] private ShopManager _manager;

    [Header("VFX Settings")]
    [SerializeField] private GameObject purchaseVFXPrefab; // Префаб для VFX эффекта
    [SerializeField] private Transform vfxOffset; // Смещение для VFX

    private bool isremoved = true;
    public uint Price => price;
    public string DrinkName => drinkName;

    private void OnEnable()
    {
        if (isremoved == false)
            Destroy(gameObject);

        if (drinkImage != null)
            drinkImage.sprite = sprite;

        if (drinkNameText != null)
            drinkNameText.text = drinkName;

        if (drinkPriceText != null)
            drinkPriceText.text = "$" + price;

        if (buyButton != null)
            buyButton.onClick.AddListener(PurchaseDrink);

        if (removeButton != null)
            removeButton.onClick.AddListener(RemoveDrink);

        // Деактивируем кнопку удаления по умолчанию
        if (removeButton != null)
            removeButton.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (buyButton != null)
            buyButton.onClick.RemoveListener(PurchaseDrink);

        if (removeButton != null)
            removeButton.onClick.RemoveListener(RemoveDrink);
    }

    public void Setup(DrinkSlot original)
    {
        drinkImage.sprite = original.drinkImage.sprite;
        drinkNameText.text = original.DrinkName;
        drinkPriceText.text = "$" + original.Price;

        if (buyButton != null)
        {
            buyButton.gameObject.SetActive(false);
        }
    }

    public void SetQuantity(int quantity)
    {
        if (drinkQuantityText != null)
        {
            drinkQuantityText.text = "x" + quantity;
        }
    }

    public void EnableRemoveButton()
    {
        if (removeButton != null)
        {
            removeButton.gameObject.SetActive(true); // Включаем кнопку
            removeButton.onClick.RemoveAllListeners(); // Очищаем старые подписки
            removeButton.onClick.AddListener(() =>
            {
                // Уведомляем контейнер об удалении
                var container = FindObjectOfType<PurchasedDrinkContainer>();
                if (container != null)
                {
                    container.AnimateAndRemoveDrink(this); // Удаляем с анимацией
                }
            });
        }
        else
        {
            Debug.LogWarning("Remove button is not assigned in the DrinkSlot prefab!");
        }
    }

    private void PurchaseDrink()
    {
        PlayPurchaseVFX();
        // Анимация движения
        GameObject animatedCopy = Instantiate(drinkImage.gameObject, drinkImage.transform.parent);
        animatedCopy.transform.position = drinkImage.transform.position;

        animatedCopy.transform.DOMove(purchaseTarget.position, 0.5f).OnComplete(() =>
        {
            
            Destroy(animatedCopy);
            _manager.AddPurchasedDrink(this);

            // Воспроизведение VFX
            
        });
    }

    private void PlayPurchaseVFX()
    {
        if (purchaseVFXPrefab != null)
        {
            GameObject vfxInstance = Instantiate(purchaseVFXPrefab, vfxOffset.position, Quaternion.identity, vfxOffset);

            // Уничтожение VFX объекта после завершения
            Destroy(vfxInstance, 10f); // Время зависит от длительности VFX
        }
        else
        {
            Debug.LogWarning("Purchase VFX Prefab is not assigned!");
        }
    }

    private void RemoveDrink()
    {
        isremoved = false;
        // Уведомляем контейнер об удалении напитка
        PurchasedDrinkContainer container = GetComponentInParent<PurchasedDrinkContainer>();

        container.AnimateAndRemoveDrink(this);
    }
}
