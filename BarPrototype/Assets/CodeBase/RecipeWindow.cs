using UnityEngine;
using UnityEngine.UI;

public class RecipeWindow : MonoBehaviour
{
    [SerializeField] private Image drinkImage; // Image of the drink
    [SerializeField] private Text drinkNameText; // Name of the drink
    [SerializeField] private Text drinkDescriptionText; // Description of the drink

    [SerializeField] private Button closeButton; // Button to close the window

    private void OnEnable()
    {
        // Initially, the window will be hidden
        //gameObject.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseWindow);
    }

    private void OnDisable()
    {
        if (closeButton != null)
            closeButton.onClick.RemoveListener(CloseWindow);
    }

    // This method fills the RecipeWindow with the recipe data
    public void SetupRecipe(string name, string description, Sprite image)
    {
        if (drinkNameText != null)
            drinkNameText.text = name;

        if (drinkDescriptionText != null)
            drinkDescriptionText.text = description;

        if (drinkImage != null)
            drinkImage.sprite = image;

        // Show the window
        gameObject.SetActive(true);
    }

    private void CloseWindow()
    {
        // Close the recipe window
        gameObject.SetActive(false);
    }
}
