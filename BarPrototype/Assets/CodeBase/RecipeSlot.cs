using UnityEngine;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour
{
    [SerializeField] private Image drinkImage;
    [SerializeField] private Text drinkNameText;
    [SerializeField] private Text drinkPriceText;
    [SerializeField] private Text drinkDescriptionText;
    [SerializeField] private Button viewRecipeButton; // Button to view the recipe in the window
    [SerializeField] private Sprite sprite; // Image of the drink
    [SerializeField] private string drinkName; // Name of the drink
    [SerializeField] private uint price; // Price of the drink
    [SerializeField] private string drinkDescription; // Description of the drink

    [SerializeField] private RecipeWindow recipeWindow; // Reference to the RecipeWindow class

    private void OnEnable()
    {
        if (drinkImage != null)
            drinkImage.sprite = sprite;

        if (drinkNameText != null)
            drinkNameText.text = drinkName;

        if (drinkPriceText != null)
            drinkPriceText.text = "$" + price;

        if (drinkDescriptionText != null)
            drinkDescriptionText.text = drinkDescription;

        if (viewRecipeButton != null)
            viewRecipeButton.onClick.AddListener(OpenRecipeWindow);
    }

    private void OnDisable()
    {
        if (viewRecipeButton != null)
            viewRecipeButton.onClick.RemoveListener(OpenRecipeWindow);
    }

    private void OpenRecipeWindow()
    {
        if (recipeWindow != null)
        {
            // Send the drink data to the RecipeWindow
            recipeWindow.SetupRecipe(drinkName, drinkDescription, sprite);
        }
    }
}
